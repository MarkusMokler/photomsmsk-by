using System;
using System.Web.Http;
using Fotobel.Models;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;

namespace Fotobel.Api.Version2.Hall
{
    public class ScheduleDayController : ApiController
    {
        private readonly AppContext _context = new AppContext();

        [HttpPut]
        public IHttpActionResult Put(Guid shortcut, Guid id, Proxy<ScheduleDay> model)
        {
            var entity = _context.ScheduleDays.Find(id);
            model.Patch(entity);
            _context.SaveChanges();
            return Ok(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}