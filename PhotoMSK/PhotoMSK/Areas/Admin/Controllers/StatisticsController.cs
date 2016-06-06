using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoMSK.Areas.Admin.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Admin/Statistics
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserRating()
        {
            return View();
        }

        public ActionResult Pro()
        {
            return View();
        }
    }
}