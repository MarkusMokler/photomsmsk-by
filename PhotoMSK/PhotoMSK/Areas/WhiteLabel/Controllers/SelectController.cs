using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Models;

namespace PhotoMSK.Areas.WhiteLabel.Controllers
{

    [Authorize]
    public class SelectController : WhiteLabelController
    {
        private readonly AppContext _context = new AppContext();
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();

            var userinfo = _context.UserInformations.SingleOrDefault(x => x.User.Id == id);

            if (userinfo == null)
                throw new Exception();

            var elems = userinfo.Roles.Where(x => x.AccessStatus <= AccessStatus.Administrator).Select(x => x.Route).ToList();

            return View(elems);
        }


        public ActionResult Set(Guid ID)
        {
            RouteID = ID;
            return Redirect("/WhiteLabel/Home/Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}