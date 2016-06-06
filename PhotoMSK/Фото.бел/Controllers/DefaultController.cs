using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fotobel.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Shortcut(string id)
        {
            return View();
        }

        public ActionResult CalendarFrame(string shortcut)
        {
            return View();
        }
    }
}