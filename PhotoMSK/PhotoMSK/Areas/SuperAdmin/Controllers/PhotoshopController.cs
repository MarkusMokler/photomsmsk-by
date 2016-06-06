using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Models;

namespace PhotoMSK.Areas.SuperAdmin.Controllers
{
    public class PhotoshopController : Controller
    {
        AppContext _context = new AppContext();
        // GET: SuperAdmin/Photoshop
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Category()
        {
            var kvp = new KeyValuePair<IList<Category>, IList<Brand>>(
                _context.Categories.ToList(), _context.Brands.ToList());
            return View(kvp);
        }
        public ActionResult Product()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}