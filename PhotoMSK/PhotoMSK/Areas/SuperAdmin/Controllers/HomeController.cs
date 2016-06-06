using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Models;

namespace PhotoMSK.Areas.SuperAdmin.Controllers
{
    public class HomeController : Controller
    {
        readonly AppContext _context = new AppContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddView()
        {
            foreach (var elem in _context.Routes.ToList())
            {
                elem.AddView();
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UploadView()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UploadView(DbRazorView model)
        {
            model.ID = Guid.NewGuid();
            _context.Razorviews.Add(model);
            _context.SaveChanges();
            return View(model);
        }
    }
}