using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using PhotoMSK.Areas.Default.ViewData.Photoshop;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Extensiolns;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Routes;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class PhotoshopController : Controller
    {
        private readonly AppContext _context = new AppContext();
        private IPhotoshopService _photoshopService;

        public PhotoshopController(IPhotoshopService photoshopService)
        {
            _photoshopService = photoshopService;
        }

        public async Task<ActionResult> Index()
        {
            var data = _photoshopService.Where();

            var model = new IndexViewData
            {
                Page = data
            };

            return View(model);
        }

        public async Task<ActionResult> Details(string shortcut)
        {
            if (shortcut == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Photoshop photoshop = await _context.Photoshops.SingleOrDefaultAsync(x => x.Shortcut == shortcut);

            if (photoshop == null)
                return HttpNotFound();

            var model = new DetailsViewData
            {
                RouteDetails = Mapper.Map<PhotoshopViewModel.Details>(photoshop)
            };

            //       model.Categorieses = _context.Categories.ToList().As<IList<CategoryViewModel>>();
            return View(model);
        }

        public async Task<ActionResult> About(string shortcut)
        {
            if (shortcut == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Photoshop photoshop = await _context.Photoshops.SingleOrDefaultAsync(x => x.Shortcut == shortcut);

            if (photoshop == null)
                return HttpNotFound();

            var model = new AboutViewData
            {
                Item = Mapper.Map<PhotoshopViewModel.Summary>(photoshop)
            };
            //         model.Categorieses = _context.Categories.ToList().As<IList<CategoryViewModel>>();
            return View(model);
        }

        public async Task<ActionResult> Contact(string shortcut)
        {
            if (shortcut == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Photoshop photoshop = await _context.Photoshops.SingleOrDefaultAsync(x => x.Shortcut == shortcut);

            if (photoshop == null)
                return HttpNotFound();

            var model = new ContactViewData
            {
                Item = Mapper.Map<PhotoshopViewModel.Summary>(photoshop)
            };
            //     model.Categorieses = _context.Categories.ToList().As<IList<CategoryViewModel>>();
            return View(model);
        }

        public async Task<ActionResult> Catalog(string shortcut)
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photoshop photoshop = await _context.Photoshops.SingleOrDefaultAsync(x => x.Shortcut == shortcut);
            if (photoshop == null)
            {
                return HttpNotFound();
            }
            this.SetTheme(photoshop);
            var model = photoshop.As<PhotoshopViewModel.Summary>();
            //    model.Categorieses = _context.Categories.ToList().As<IList<CategoryViewModel>>();
            return View(model);
        }
        public async Task<ActionResult> ProductDetails(string shortcut)
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photoshop photoshop = await _context.Photoshops.SingleOrDefaultAsync(x => x.Shortcut == shortcut);
            if (photoshop == null)
            {
                return HttpNotFound();
            }
            this.SetTheme(photoshop);
            var model = photoshop.As<PhotoshopViewModel.Summary>();
            //       model.Categorieses = _context.Categories.ToList().As<IList<CategoryViewModel>>();
            return View(model);
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