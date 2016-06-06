using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using PhotoMSK.Areas.Default.ViewData.Photostudio;
using PhotoMSK.Data;
using PhotoMSK.Extensiolns;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class PhotostudioController : Controller
    {
        private readonly AppContext _context = new AppContext();
        private IPhotostudioService _photostudioService;

        public PhotostudioController(IPhotostudioService photostudioService)
        {
            _photostudioService = photostudioService;
        }

        public ActionResult Index()
        {

            var data = _photostudioService.Where();

            var model = new IndexViewData
            {
                Page = data
            };

            return View(model);
        }

        public ActionResult Details(string shortcut)
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var photostudio = _context
                .Photostudios
                .SingleOrDefault(x => x.Shortcut == shortcut);

            if (photostudio == null)
            {
                return HttpNotFound();
            }

            var viewData = new DetailsViewData
            {
                Item = Mapper.Map<PhotostudioViewModel.Detaills>(photostudio)
            };

            return View(viewData);
        }

        public ActionResult Details2(string shortcut)
        {
            var photostudio = _context
                .Photostudios
                .SingleOrDefault(x => x.Shortcut == shortcut);

            if (photostudio == null)
            {
                return HttpNotFound();
            }

            var viewData = new DetailsViewData
            {
                Item = Mapper.Map<PhotostudioViewModel.Detaills>(photostudio)
            };

            return View(viewData);
        }
        public ActionResult Details3(string shortcut)
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var photostudio = _context
                .Photostudios
                .SingleOrDefault(x => x.Shortcut == shortcut);

            if (photostudio == null)
            {
                return HttpNotFound();
            }

            var viewData = new DetailsViewData
            {
                Item = Mapper.Map<PhotostudioViewModel.Detaills>(photostudio)
            };

            return View(viewData);
        }

        public ActionResult IframeHalls(string shortcut)
        {
            var studio = _context.Photostudios
                .SingleOrDefault(x => x.Shortcut == shortcut);

            if (studio == null)
            {
                return HttpNotFound();
            }

            var viewData = new IframeHallsViewData
            {
                Item = Mapper.Map<PhotostudioViewModel.Detaills>(studio)
            };

            return View(viewData);
        }

        public ActionResult Vkontakte(string shortcut)
        {
            var photostudio = _context
                .Photostudios
                .SingleOrDefault(x => x.Shortcut == shortcut);

            if (photostudio == null)
            {
                return HttpNotFound();
            }

            var viewData = new VkontakteViewData
            {
                Item = Mapper.Map<PhotostudioViewModel.Detaills>(photostudio)
            };

            return View(viewData);
        }

        public ActionResult Halls()
        {
            var query = _context.Halls
                .Include(x => x.Photos)
                .Include(y => y.Calendar)
                .Include(z => z.Schedule)
                .Include(i => i.Photostudio)
                .ToList();


            var model = new HallsViewData
            {
                Page = new PageView<HallViewModel.Summary>
                {
                    Elements = ObjectExtension.As<IList<HallViewModel.Summary>>(query),
                    ItemsCount = 20,
                    PageSize = 20
                }
            };

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