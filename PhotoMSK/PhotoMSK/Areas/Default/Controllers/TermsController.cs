using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class TermsController : Controller
    {
        // GET: Default/Terms
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Rules()
        {
            return View();
        }
        public ActionResult Agreement()
        {
            return View();
        }
    }
}