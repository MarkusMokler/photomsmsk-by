using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoMSK.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Admin/Blog/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
	}
}