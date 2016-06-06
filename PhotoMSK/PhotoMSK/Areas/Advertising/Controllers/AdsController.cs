using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoMSK.Areas.Advertising.Controllers
{
    public class AdsController : Controller
    {
        // GET: Advertising/Ads
        public ActionResult Index()
        {
            return View();
        }
    }
}