using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoMSK.Data;
using PhotoMSK.Models;

namespace PhotoMSK.Areas.SuperAdmin.Controllers
{
    public class CategoryController : Controller
    {
        AppContext _context = new AppContext();
        // GET: SuperAdmin/Category
        public ActionResult Index()
        {
            
            return View(_context.Categories.ToList());
        }
        public ActionResult Create()
        {
            return View(_context.Categories.ToList());
        }
        public ActionResult Edit()
        {
            return View();
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