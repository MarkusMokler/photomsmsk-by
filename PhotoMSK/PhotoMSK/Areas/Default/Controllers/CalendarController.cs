using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using PhotoMSK.Areas.Default.ViewData.Calendar;
using PhotoMSK.Data;
using PhotoMSK.Extensiolns;
using PhotoMSK.ViewModels;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class CalendarController : Controller
    {
        private readonly AppContext db = new AppContext();

        public ActionResult Index()
        {
            var calendars = db.Calendars.ToList().As<IList<CalendarViewModel>>();

            var model = new IndexViewData
              {
                  Page =
                  {
                      Elements = calendars,
                      ItemsCount = calendars.Count(),
                      PageSize = 20
                  }
              };

            return View(model);
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var calendar = db.Calendars.Find(id);

            if (calendar == null)
            {
                return HttpNotFound();
            }

            var model = new DetailsViewData
            {
                Item = Mapper.Map<CalendarViewModel>(calendar)
            };

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}