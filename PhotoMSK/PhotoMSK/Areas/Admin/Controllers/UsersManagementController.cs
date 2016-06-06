using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoMSK.Areas.Admin.Controllers
{
    public class UsersManagementController : Controller
    {
        // GET: Admin/UsersManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Roles()
        {
            return View();
        }

        public ActionResult Permission()
        {
            return View();
        }
    }
}