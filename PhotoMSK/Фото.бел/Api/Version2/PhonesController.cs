using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;

namespace Fotobel.Api.Version2
{
    public class PhonesController : ApiController
    {
        private readonly AppContext _db = new AppContext();

        [HttpPost]
        public IHttpActionResult ValidatePhone(PhoneRequestModel model)
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
                _db.Phones.Add(new Phone
                  {
                      ID = Guid.NewGuid(),
                      Number = number,
                      RoutesPhone = new List<RoutesPhone>(){ 
                        new RoutesPhone()
                        {
                            ID = Guid.NewGuid(),
                            Confirm = false,
                            ConfirmCode = code,
                            DateAdded = DateTime.Now,
                            EntityID = model.ID
                        }}
                  });

                _db.SaveChanges();

               // assistent.SendMessage(number, string.Format("Ваш проверочный код {0}", code));
                return Ok(new { message = "Проверочный код выслан" });

            }
            if (phone.RoutesPhone.Any(x => x.EntityID == model.ID))
                return Ok(new { message = "Такой номер уже привязан к студии" });
            phone.RoutesPhone.Add(new RoutesPhone
            {
                ID = Guid.NewGuid(),
                Confirm = false,
                ConfirmCode = code,
                DateAdded = DateTime.Now,
                EntityID = model.ID
            });
            _db.SaveChanges();
           // assistent.SendMessage(number, string.Format("Ваш проверочный код {0}", code));
            return Ok(new { message = "Проверочный код выслан" });
        }

        [HttpDelete]
        public IHttpActionResult DeletePhone(PhoneRequestModel model)
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
            if(phone == null)
                return Ok(new { Error = 1, Message = "Error" });
            
            var Info = phone.RoutesPhone.SingleOrDefault(x => x.EntityID == model.ID);
            if (Info == null)
                return Ok(new { Error = 1, Message = "Error" });

            _db.RoutesPhones.Attach(Info);
            _db.Entry(Info).State = EntityState.Deleted;

            _db.Phones.Attach(phone);
            _db.Entry(phone).State = EntityState.Deleted;

            _db.SaveChanges();
           
           return Ok(new { message = "Телефон удален" });

        }

        [HttpPost]
        public IHttpActionResult SubmitPhone(PhoneRequestModel model)
        {
            var phone =
             _db.RoutesPhones.SingleOrDefault(x => x.EntityID == model.ID && x.ConfirmCode == model.ConfirmCode);
            if (phone == null)
                return Ok(new { Error = 1, Message = "не верный код валидации" });
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
    public class PhoneRequestModel
    {
        public string Number { get; set; }
        public Guid ID { get; set; }
        public int ConfirmCode { get; set; }
    }
}