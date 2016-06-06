using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class LandingController : Controller
    {
        // GET: Default/Landing
        public ActionResult Photographer()
        {
            return View();
        }
    }
}