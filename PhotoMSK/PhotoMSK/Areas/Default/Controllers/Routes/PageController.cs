using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using PhotoMSK.Areas.Default.ViewData.Page;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class PageController : Controller
    {
        private readonly AppContext _context = new AppContext();
        private IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        // GET: /PhototechnicsModel/
        public ActionResult Index(int pageSize = 20, int @from = 0)
        {
            var data = _pageService.Where();

            var model = new IndexViewData
            {
                Page = data
            };

            return View(model);
        }

        public async Task<ActionResult> Details(string shortcut)
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var page = await _context.Pages.FirstOrDefaultAsync(x => x.Shortcut == shortcut);

            if (page == null)
                return HttpNotFound();

            var model = new DetailsViewData
            {
                Item = Mapper.Map<PublicViewModel.Details>(page)
            };

            return View(model);
        }


        [Authorize]
        public async Task<ActionResult> Edit(string shortcut)
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var page = await _context.Pages.FirstOrDefaultAsync(x => x.Shortcut == shortcut);

            if (page == null)
                return HttpNotFound();

            var model = new DetailsViewData
            {
                Item = Mapper.Map<PublicViewModel.Details>(page)
            };

            return View(model);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Page phototechnics)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(phototechnics).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(phototechnics);
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
            return View(page);
        }


        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var phototechnics = await _context.Pages.FindAsync(id);
            _context.Routes.Remove(phototechnics);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
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