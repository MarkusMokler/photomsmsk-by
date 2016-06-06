using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PhotoMSK.Areas.Default.ViewData.Catalog;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Extensiolns;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Routes;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class WhiteLabelController : RouteController
    {
        private readonly IMenuService _menuService;

        public WhiteLabelController(IPhototechnicsService phototechnicsService, IMenuService menuService)
            : base(phototechnicsService)
        {
            _menuService = menuService;
        }

        public ActionResult Index(string whitelabel = "")
        {
            var elem = Context.Routes.Include(x => x.Raiting)
                .Single(x => x.Domain == whitelabel);

            elem.AddView();
            this.SetTheme(elem);

            Context.SaveChanges();

            return View(elem);
        }

        public ActionResult Pagewl(string whitelabel = "", string id = "")
        {
            if (whitelabel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrEmpty(id))
            {
                return Index(whitelabel);
            }

            RouteEntity entity = Context.Routes.SingleOrDefault(x => x.Domain == whitelabel);
            if (entity == null)
            {
                return HttpNotFound();
            }
            this.SetTheme(entity);
            if (entity is Photoshop)
            {
                var model = entity.As<PhotoshopViewModel.WhiteLabel>();

                var page = Context.TextPages.FirstOrDefault(x => x.RouteID == model.ID && x.Slug == id);

                var mmodel = new PageViewData()
                {
                    Menu = _menuService.GetMenuForRoute(page.ID),
                    Route = model,
                    PageView = page.As<TextPageViewModel.Details>()
                };
                return View("Page", mmodel);
            }
            if (entity is Photorent)
            {
                var model = entity.As<PhotorentViewModel.Details>();

                var page = Context.TextPages.FirstOrDefault(x => x.RouteID == model.ID && x.Slug == id);

                var mmodel = new PageViewData()
                {
                    Menu = _menuService.GetMenuForRoute(page.ID),
                    Route = model,
                    PageView = page.As<TextPageViewModel.Details>()
                };
                return View("Page", mmodel);
            }
            return View("Page");
        }

        public ActionResult Page(string shortcut = "", string id = "")
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrEmpty(id))
            {
                return Index(shortcut);
            }

            RouteEntity entity = Context.Routes.SingleOrDefault(x => x.Shortcut == shortcut);
            if (entity == null)
            {
                return HttpNotFound();
            }
            this.SetTheme(entity);
            if (entity is Photoshop)
            {
                var link = entity as Photoshop;
                var model = entity.As<PhotoshopViewModel.WhiteLabel>();

                var page = Context.TextPages.FirstOrDefault(x => x.RouteID == model.ID && x.Slug == id);

                var mmodel = new PageViewData()
                {
                    Route = model,
                    PageView = page.As<TextPageViewModel.Details>()
                };
                return View("Page", mmodel);
            }
            if (entity is Photorent)
            {
                var link = entity as Photorent;
                var model = entity.As<PhotorentViewModel.Details>();

                var page = Context.TextPages.FirstOrDefault(x => x.RouteID == model.ID && x.Slug == id);

                var mmodel = new PageViewData()
                {
                    Route = model,
                    PageView = page.As<TextPageViewModel.Details>()
                };
                return View("Page", mmodel);
            }
            return View("Page");
        }

    }
}