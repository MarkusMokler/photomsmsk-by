using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoMSK.Areas.Admin.Controllers
{
    public class WhiteLabelController : Controller
    {
        // GET: Admin/WhiteLabel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PageEdit()
        {
            return View();
        }
    }
}