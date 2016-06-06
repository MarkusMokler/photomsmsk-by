using System;
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
    public class PageController : Controller
    {
        private readonly AppContext _context = new AppContext();
        public async Task<ActionResult> Index()
        {
            return View(await _context.Pages.ToListAsync());
        }

        public async Task<ActionResult> Details(string shortcut)
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page phototechnics = await _context.Pages.FirstOrDefaultAsync(x => x.Shortcut == shortcut);
            if (phototechnics == null)
            {
                return HttpNotFound();
            }
            return View(phototechnics);
        }

        [Authorize]
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
            Page page = await _context.Pages.SingleOrDefaultAsync(x => x.Shortcut == shortcut);
            if (page == null)
            {
                return HttpNotFound();
            }

            return View(page.As<PageEditModel>());
        }

        [Authorize]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page.As<PageEditModel>());
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