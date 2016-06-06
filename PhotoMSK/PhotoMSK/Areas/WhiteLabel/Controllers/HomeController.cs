using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoMSK.Data;
using PhotoMSK.Models;
using PhotoMSK.Models.Attributes;

namespace PhotoMSK.Areas.WhiteLabel.Controllers
{
    [Authorize]
    [RouteID]
    public class HomeController : Controller
    {
        readonly AppContext _context = new AppContext();
    
        [RouteID]
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
    }
}