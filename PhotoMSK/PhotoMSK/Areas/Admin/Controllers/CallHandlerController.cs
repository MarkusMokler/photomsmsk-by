using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Extensiolns;
using PhotoMSK.Models;

using PhotoMSK.Models.ViewModels;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.Areas.Admin.Controllers
{
    [Authorize]
    public class CallHandlerController : Controller
    {
        readonly AppContext _context = new AppContext();
        // GET: Admin/CallHandler
        public ActionResult Index(string id = "")
        {
            var uid = User.Identity.GetUserId();
            var model = _context.Users.Find(uid);

            var mmodel = new CallHandlerViewModel();
            var phone = _context.Phones.SingleOrDefault(x => x.Number == "+" + id);

            if (phone != null)
                mmodel.UserInformation = phone.UserPhone.User.As<UserInformationViewModel.Details>();

            mmodel.Phone = "+" + id;
            mmodel.Photostudios = model.UserInformation.Roles.Where(x => x.AccessStatus == AccessStatus.Owner || x.AccessStatus == AccessStatus.Administrator).Select(x => x.Route).ToList().Where(x => x.Shortcut == "photolab").OfType<Photostudio>().As<IList<PhotostudioViewModel.Detaills>>().ToList();
            mmodel.Photorents = model.UserInformation.Roles.Where(x => x.AccessStatus == AccessStatus.Owner || x.AccessStatus == AccessStatus.Administrator).Select(x => x.Route).OfType<Photorent>().As<IList<PhotorentViewModel.Details>>().ToList();

            return View(mmodel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}