using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2.Route
{
    public class PhoneCheckController : ApiController
    {
        private readonly AppContext _db = new AppContext();
        private ISmsAssistent _assistent;
        
        public PhoneCheckController(ISmsAssistent assistent)
        {
            _assistent = assistent;
        }
        [HttpPost]
        public IHttpActionResult ValidatePhone(string shortcut, [FromBody] PhoneRequestModel phoneRequest)
        {
            if (phoneRequest.Number == null)
            {
                return Ok(new { message = "Введите пожалуйста номер" });
            }
            var number = phoneRequest.Number.Replace("-", "").Replace(" ", "");

            if (number.StartsWith("80"))
            {
                var regex = new Regex(Regex.Escape("80"));
                number = regex.Replace(number, "+375", 1);
            }
            if (number.StartsWith("+") == false)
            {
                number = "+" + number;
            }

            var rt = _db.Routes.Single(x => x.Shortcut == shortcut);
            rt.Phones.Add(new RoutesPhone
            {
                EntityID = phoneRequest.EntityID,
                ID = Guid.NewGuid(),
                Entity = _db.Routes.Find(phoneRequest.EntityID),
                Phone = new Phone
                {
                    ID=Guid.NewGuid(),
                    Number = phoneRequest.Number,
                    DateLastSending = DateTime.Now,
                }
            });
            try
            {
                _db.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                throw new ValidationException("Такой телефон уже используется");
            }
            
            var route = _db.Routes.Single(x => x.ID == phoneRequest.EntityID);
            try
            {
                if (route.GetPhones().Any(x => x.Number == number))
                {
                    int code = new Random().Next(0, 10000);

                    //RoutesPhone rPhone = _db.Phones.First(x => x.Number == number).RoutesPhone.First(x => x.EntityID == phoneRequest.EntityID);
                    var rPhone = _db.Routes.First(x=>x.Shortcut==shortcut).Phones.First(x=>x.EntityID==phoneRequest.EntityID);
                    var rObj = _db.Routes.First(x => x.Shortcut == shortcut);
                    rObj.City = phoneRequest.City;
                    rObj.Address = phoneRequest.Adress;
                    rPhone.Confirm = false;
                    rPhone.ConfirmCode = code;
                    _db.SaveChanges();
                    _assistent.SendMessage(number, string.Format("Ваш проверочный код {0}", code));
                    return Ok(new { message = "Проверочный код выслан", action = 0 });
                }
                throw new Exception();
            }
            catch (Exception)
            {
                return Ok(new { message = "Пожалуйста введите свой телефон" });
            }
        }

        [HttpPut]
        public IHttpActionResult ConfirmPhone(string shortcut, [FromBody]PhoneRequestModel phoneRequest)
        {
            var phone = _db.RoutesPhones
                .SingleOrDefault(x => x.EntityID == phoneRequest.EntityID && x.ConfirmCode == phoneRequest.ConfirmCode);

            try
            {
                if (phone == null)
                    throw new ArgumentNullException();

                phone.Confirm = true;
                _db.SaveChanges();

                return Ok(new { action = 1, message = "Телефон успешно подтвержден" });
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Ошибка!");
            }
        }
    }

    public class PhoneRequestModel
    {
        public string City { get; set; }
        public string Adress { get; set; }
        public string Number { get; set; }
        public Guid EntityID { get; set; }
        public int ConfirmCode { get; set; }
    }
}