using System.Linq;
using System.Net;
using System.Web.Mvc;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Extensiolns;
using PhotoMSK.Models;
using PhotoMSK.Models.EditViewModel.Routes;

namespace PhotoMSK.Areas.Admin.Controllers
{
    [Authorize]
    public class MasterclassController : Controller
    {
        private readonly AppContext _context = new AppContext();

        public ActionResult Index(string shortcut)
        {
            var studio = _context.Masterclasses.Single(x => x.Shortcut == shortcut);
            return View(studio);
        }

        public ActionResult Create()
        {
            return View();
        }


        public ActionResult Edit(string shortcut)
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Masterclass masterclass = _context.Masterclasses.SingleOrDefault(x => x.Shortcut == shortcut);
            if (masterclass == null)
            {
                return HttpNotFound();
            }
            return View(masterclass.As<MasterclassEditModel>());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}