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
    public class PhotographerController : Controller
    {
        private readonly AppContext _context = new AppContext();

        public ActionResult Index(string shortcut)
        {
            var studio = _context.Photographers.Single(x => x.Shortcut == shortcut);
            return View(studio);
        }

        public ActionResult Index2(string shortcut)
        {
            var studio = _context.Photographers.Single(x => x.Shortcut == shortcut);
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
            Photographer photographer = _context.Photographers.SingleOrDefault(x => x.Shortcut == shortcut);
            if (photographer == null)
            {
                return HttpNotFound();
            }
            return View(photographer.As<PhotographerEditModel>());
        }

        public ActionResult Delete(string shortcut)
        {
            var photographer = _context.Photographers.Find(shortcut);

            photographer.Active = false;
            _context.SaveChanges();
            return Redirect("/Admin");
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