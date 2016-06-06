using System;
using System.Linq;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;

namespace Fotobel.Api.Services
{
    public class CallbackController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpPost]
        public IHttpActionResult Post(CallbackModel model)
        {
            var rmodel = _context.Routes.Find(model.RouteID);
            var phone = rmodel.Phones.First().Phone.Number;


            string template = string.IsNullOrEmpty(model.Name) ? "Перезвонить , {1} от {2}" : "Перезвонить {0}, {1} от {2}";


            _context.SmsMessages.Add(new SmsMessage
            {
                ID = Guid.NewGuid(),
                Message = string.Format(template, model.Name, model.Phone, rmodel.GetName()),
                Phone = phone
            });

            _context.SaveChanges();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
    }

    public class CallbackModel
    {
        public Guid RouteID { get; set; }
        public String Name { get; set; }
        public string Phone { get; set; }
    }
}
