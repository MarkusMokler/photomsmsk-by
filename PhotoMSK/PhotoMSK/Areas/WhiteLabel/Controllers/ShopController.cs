using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoMSK.Models.Attributes;

namespace PhotoMSK.Areas.WhiteLabel.Controllers
{
    [Authorize]
    [RouteID]
    public class ShopController : Controller
    {
        // GET: WhiteLabel/Shop
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products()
        {
            return View();
        }

        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult Settings()
        {
            return View();
        }
        public ActionResult Reports()
        {
            return View();
        }
    }
}