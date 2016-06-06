using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class HelpController : Controller
    {
        // GET: Default/Help
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RestTest()
        {
            return View();
        }
    }
}