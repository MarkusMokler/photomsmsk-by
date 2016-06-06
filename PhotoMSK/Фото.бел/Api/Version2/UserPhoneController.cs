using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Interfaces;

namespace Fotobel.Api.Version2
{
    public class UserPhoneController : ApiController
    {
        private readonly AppContext _db = new AppContext();
        private ISmsAssistent _assistent;

        public UserPhoneController(ISmsAssistent assistent)
        {
            _assistent = assistent;
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult ValidatePhone(UserPhonesPhoneRequestModel model)
        {
            var number = model.Number.Replace("-", "").Replace(" ", "");

            if (number.StartsWith("80"))
            {
                var regex = new Regex(Regex.Escape("80"));
                number = regex.Replace(number, "+375", 1);
            }
            if (number.StartsWith("+") == false)
            {
                number = "+" + number;
            }


            Phone phone = _db.Phones.SingleOrDefault(x => x.Number == number);
            int code = new Random().Next(0, 10000);

            if (phone == null)
            {
                var user = _db.UserInformations.Find(model.ID);
                var userPhone = new UserPhone()
                {
                    Confirm = false,
                    ConfirmCode = code,
                    DateAdded = DateTime.Now,
                    User = _db.UserInformations.Find(model.ID),
                    UserID = model.ID,
                    ID = Guid.NewGuid(),
                    Phone = new Phone()
                    {
                        ID = Guid.NewGuid(),
                        DateLastSending = DateTime.Now,
                        Number = number
                    }
                };
                user.Phones.Add(userPhone);
                _db.SaveChanges();

                _assistent.SendMessage(number, string.Format("Ваш проверочный код {0}", code));
                return Ok(new {message = "Проверочный код выслан"});

            }
            return Ok(new { Error = 1, message = "Такой номер уже используется" });
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult DeletePhone(UserPhonesPhoneRequestModel model)
        {

            var number = model.Number.Replace("-", "").Replace(" ", "");

            if (number.StartsWith("80"))
            {
                var regex = new Regex(Regex.Escape("80"));
                number = regex.Replace(number, "+375", 1);
            }
            if (number.StartsWith("+") == false)
            {
                number = "+" + number;
            }
            var userPhones = _db.UserInformations.Find(model.ID).Phones;
            if (userPhones.Count<2)
                return Ok(new { Error = 1, Message = "Должен быть хотя бы один номер телефона" });
            Phone phone = _db.Phones.SingleOrDefault(x => x.Number == number);
            if (phone == null)
                return Ok(new { Error = 1, Message = "Номер не существует" });

            var info = phone.UserPhone;
            if (info == null)
                return Ok(new { Error = 1, Message = "Error" });

            _db.UserPhones.Attach(info);
            _db.Entry(info).State = EntityState.Deleted;

            _db.Phones.Attach(phone);
            _db.Entry(phone).State = EntityState.Deleted;

            _db.SaveChanges();

            return Ok(new { message = "Телефон удален" });

        }
        [Authorize]
        [HttpPut]
        public IHttpActionResult SubmitPhone(UserPhonesPhoneRequestModel model)
        {
            var phone =
             _db.UserPhones.SingleOrDefault(x => x.User.ID == model.ID && x.ConfirmCode == model.ConfirmCode);
            if (phone == null)
                return Ok(new { Error = 1, Message = "Не верный код валидации" });
            phone.Confirm = true;
            _db.SaveChanges();
            return Ok(new { Message = "Телефон успешно подтвержден" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
    public class UserPhonesPhoneRequestModel
    {
        public string Number { get; set; }
        public Guid ID { get; set; }
        public int ConfirmCode { get; set; }
    }
}