using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Extensiolns;
using PhotoMSK.ViewModels;

namespace PhotoMSK.Areas.Admin.Controllers
{
    public class UserInformationController : Controller
    {
        // GET: /Admin/UserInformation/
        readonly AppContext _context = new AppContext();

        public ActionResult Index()
        {
            var id = this.User.Identity.GetUserId();
            return View(_context.Users.Find(id).UserInformation.As<UserInformationViewModel.Details>());
        }

        public ActionResult Edit()
        {
            var id = this.User.Identity.GetUserId();
            return View(_context.Users.Find(id).UserInformation);
        }
	}
}