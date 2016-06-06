using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PhotoMSK.Areas.Default.ViewData.Hall;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Extensiolns;
using PhotoMSK.ViewModels;


namespace PhotoMSK.Areas.Default.Controllers
{
    public class HallController : Controller
    {
        private readonly AppContext _context = new AppContext();

        public ActionResult Index()
        {
            var data = new HallIndex()
            {
                Page = new PageView<HallViewModel.Summary>()
                {
                    Elements = _context.Halls.ToList().As<IList<HallViewModel.Summary>>(),
                    ItemsCount = 20,
                    PageSize = 20
                }
            };

            return View(data);
        }

        public ActionResult PhotostudioHalls(string shortcut)
        {

            if (shortcut == null)
            {
                return HttpNotFound();
            }

            Photostudio studio = _context.Photostudios.SingleOrDefault(x => x.Shortcut == shortcut);

            if (studio == null)
            {
                return HttpNotFound();
            }

            ViewBag.Shortcut = shortcut;

            var data = new HallPhotostudio
            {
                Page = new PageView<HallViewModel.Details>
                {
                    Elements = studio.Halls.As<IList<HallViewModel.Details>>(),
                    ItemsCount = studio.Halls.Count
                }
            };

            return View(data);
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Hall hall = _context.Halls.Find(id);

            if (hall == null)
                return HttpNotFound();

            var data = new DetailsViewData()
             {
                 Item = hall.As<HallViewModel.Details>()
             };


            return View(data);
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