using System.Linq;
using System.Web.Mvc;
using PhotoMSK.Data;

namespace PhotoMSK.Areas.Admin.Controllers
{
    public class DeleteRouteController : Controller
    {
        readonly AppContext _context = new AppContext();

        // GET: Admin/DeleteRoute
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Confirm(string shortcut)
        {
            var model = _context.Routes.Single(x => x.Shortcut == shortcut);
            return View(model);
        }
        public ActionResult PhoneRequest(string shortcut)
        {
            var model = _context.Routes.Single(x => x.Shortcut == shortcut);
            return View(model);
        }
    }
}