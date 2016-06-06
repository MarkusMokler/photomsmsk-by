using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Extensiolns;
using PhotoMSK.Models;
using PhotoMSK.Models.EditViewModel.Routes;

namespace PhotoMSK.Areas.Admin.Controllers
{
    [Authorize]
    public class PhotorentController : Controller
    {

        private readonly AppContext _context = new AppContext();

        public ActionResult Create()
        {
            return View();
        }


        [Authorize]
        public async Task<ActionResult> Edit(string shortcut)
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photorent photorent = await _context.Photorents.SingleOrDefaultAsync(x => x.Shortcut == shortcut);
            if (photorent == null)
            {
                return HttpNotFound();
            }
            return View(photorent.As<PhotorentEditModel>());
        }


        public async Task<ActionResult> Prices(string shortcut)
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photorent photorent = await _context.Photorents.SingleOrDefaultAsync(x => x.Shortcut == shortcut);
            if (photorent == null)
            {
                return HttpNotFound();
            }
            return View(photorent);
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