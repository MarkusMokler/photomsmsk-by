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
    public class SettingsController : Controller
    {
        // GET: WhiteLabel/Settings
        public ActionResult Index()
        {
            return View();
        }
    }
}