using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using PhotoMSK.Areas.Default.ViewData.Photorent;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Extensiolns;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class PhotorentController : Controller
    {
        private readonly AppContext _context = new AppContext();
        private IPhotorentService _photorentService;

        public PhotorentController(IPhotorentService photorentService)
        {
            _photorentService = photorentService;
        }

        public async Task<ActionResult> Index()
        {
            var data = _photorentService.Where();

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

            Photorent photorent = await _context.Photorents.SingleOrDefaultAsync(x => x.Shortcut == shortcut);

            if (photorent == null)
                return HttpNotFound();

            var model = new DetailsViewData()
            {
                RouteDetails = Mapper.Map<PhotorentViewModel.Details>(photorent)
            };

            return View(model);
        }

        public async Task<ActionResult> Catalog(string shortcut)
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

            this.SetTheme(photorent);
            //     return View("Details", "_Layout", photoshop);
            return View("Catalog", photorent);
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