using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PhotoMSK.Areas.Default.ViewData.Catalog;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Data.Models.ShoppingCart;
using PhotoMSK.Extensiolns;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Routes;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class CatalogController : Controller
    {
        private readonly AppContext _context = new AppContext();
        private readonly IPhototechnicsService _phototechnicsService;
        private readonly IPhotorentService _photorentService;

        public CatalogController(IPhototechnicsService phototechnicsService, IPhotorentService photorentService)
        {
            _phototechnicsService = phototechnicsService;
            _photorentService = photorentService;
        }

        public ActionResult Index(string shortcut)
        {
            if (shortcut == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var entity = _context.Routes.SingleOrDefault(x => x.Shortcut == shortcut);

            return entity == null ? HttpNotFound() : Index(entity);
        }

        public ActionResult Indexwl(string whitelabel)
        {
            if (whitelabel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteEntity entity = _context.Routes.Include(x => x.Raiting).SingleOrDefault(x => x.Domain == whitelabel);

            return Index(entity);
        }

        public ActionResult Category(string category, string shortcut)
        {
            if (shortcut == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            RouteEntity entity = _context.Routes.SingleOrDefault(x => x.Shortcut == shortcut);

            return entity == null ? HttpNotFound() : Category(entity, category);
        }

        public ActionResult Categorywl(string category, string whitelabel)
        {
            if (whitelabel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var routeEntity = _context.Routes.SingleOrDefault(x => x.Domain == whitelabel);
            if (routeEntity == null)
            {
                return HttpNotFound();
            }

            return Category(routeEntity, category);
        }

        public ActionResult Details(string category, string slug, string shortcut)
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteEntity entity = _context.Routes.SingleOrDefault(x => x.Shortcut == shortcut);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return Details(entity, category, slug);
        }

        public ActionResult Detailswl(string category, string slug, string whitelabel)
        {
            if (whitelabel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteEntity photoshop = _context.Routes.SingleOrDefault(x => x.Domain == whitelabel);

            return photoshop != null ? Details(category, slug, photoshop.Shortcut) : new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult Cart(string shortcut)
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteEntity routeEntity = _context.Routes.SingleOrDefault(x => x.Shortcut == shortcut);

            if (routeEntity == null)
            {
                return HttpNotFound();
            }
            return Cart(routeEntity);
        }
        public ActionResult Cartwl(string whitelabel)
        {
            if (whitelabel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteEntity routeEntity = _context.Routes.SingleOrDefault(x => x.Domain == whitelabel);
            if (routeEntity == null)
            {
                return HttpNotFound();
            }
            this.SetTheme(routeEntity);
            return Cart(routeEntity);
        }

        private ActionResult Index(RouteEntity entity)
        {
            if (entity is Photoshop)
            {
                this.SetTheme(entity);
                var data = new VitrineViewData
                {
                    Menu = ObjectExtension.As<IList<MenuItemViewModel>>(_context.AbstractMenuItem.Where(x => x.RouteID == entity.ID)),
                    Route = ObjectExtension.As<PhotoshopViewModel.Summary>(entity),
                    Categorieses = ObjectExtension.As<IList<CategoryViewModel>>(_context.Categories
                        .Include(x => x.Brands.Select(y => y.Brand))
                        .ToList())
                };

                data.Technics = _phototechnicsService.GetPhotoshopTechnics(data.Route, new PageRequest<PricePosition>
                {
                    Where = x => x.PhotoshopID == entity.ID
                });

                return View("Index", data);
            }

            if (entity is Photorent)
            {
                var data = new VitrineViewData<PhotorentViewModel.Details>
                {
                    Menu = ObjectExtension.As<IList<MenuItemViewModel>>(_context.AbstractMenuItem.Where(x => x.RouteID == entity.ID))
                };

                this.SetTheme(entity);

                data.RouteDetails = ObjectExtension.As<PhotorentViewModel.Details>(entity);
                data.Categorieses = ObjectExtension.As<IList<CategoryViewModel>>
                    (_context.Categories
                    .Include(x => x.Brands.Select(y => y.Brand))
                    .ToList());

                data.Technics = _phototechnicsService.GetPhotorentTechnics(data.RouteDetails, new PageRequest<RentCalendar>
                {
                    Page = 0,
                    Size = 20,
                    Where = x => x.PhotorentID == entity.ID
                });

                return View("Index", data);
            }

            return View("Index");
        }
        private ActionResult Category(RouteEntity entity, string category)
        {
            if (entity is Photoshop)
            {
                var data = new VitrineViewData();

                data.Menu = ObjectExtension.As<IList<MenuItemViewModel>>(_context.AbstractMenuItem.Where(x => x.RouteID == entity.ID));
                this.SetTheme(entity);
                data.Route = ObjectExtension.As<PhotoshopViewModel.Summary>(entity);
                data.Categorieses = ObjectExtension.As<IList<CategoryViewModel>>(_context.Categories
                    .Include(x => x.Brands.Select(y => y.Brand))
                    .ToList());

                data.Technics = _phototechnicsService.GetPhotoshopTechnics(data.Route, new PageRequest<PricePosition>
                {
                    Page = 0,
                    Size = 20,
                    Where = x => x.PhotoshopID == entity.ID && x.Phototechnics.Category.Slug == category
                });

                return View("Index", data);
            }
            if (entity is Photorent)
            {
                var data = new VitrineViewData<PhotorentViewModel.Details>
                {
                    Menu = ObjectExtension.As<IList<MenuItemViewModel>>(_context.AbstractMenuItem.Where(x => x.RouteID == entity.ID)),
                    RouteDetails = ObjectExtension.As<PhotorentViewModel.Details>(entity),
                    Categorieses = ObjectExtension.As<IList<CategoryViewModel>>(_context.Categories
                    .Include(x => x.Brands.Select(y => y.Brand))
                    .ToList())
                };

                this.SetTheme(entity);

                data.Technics = _phototechnicsService.GetPhotorentTechnics(data.RouteDetails, new PageRequest<RentCalendar>
                {
                    Where = x => x.PhotorentID == entity.ID && x.Phototechnics.Category.Slug == category
                });

                return View("Index", data);
            }

            return View("Index");
        }
        private ActionResult Details(RouteEntity entity, string category, string slug)
        {
            if (entity is Photoshop)
            {
                var data = new ProductViewData
            {
                Menu = ObjectExtension.As<IList<MenuItemViewModel>>(_context.AbstractMenuItem.Where(x => x.RouteID == entity.ID))
            };
                this.SetTheme(entity);
                var link = entity as Photoshop;
                data.Route = ObjectExtension.As<PhotoshopViewModel.Summary>(entity);

                data.PhototechnicsViewModel = ObjectExtension.As<PhototechnicsViewModel.Details>(_context.PricePositions.Where(x => x.PhotoshopID == entity.ID && x.Phototechnics.Shortcut == slug)
                        .Include(x => x.Phototechnics.ParameterValues).Include(x => x.Phototechnics.ParameterValues.Select(y => y.Parameter)).FirstOrDefault());
                return View("Details", data);
            }
            if (entity is Photorent)
            {
                var data = new ProductViewData
                {
                    Menu = ObjectExtension.As<IList<MenuItemViewModel>>(_context.AbstractMenuItem.Where(x => x.RouteID == entity.ID))
                };
                this.SetTheme(entity);
                var link = entity as Photorent;
                data.Route = ObjectExtension.As<PhotorentViewModel.Details>(entity);

                data.Categorieses = ObjectExtension.As<IList<CategoryViewModel>>(_context.Categories.ToList());

                data.PhototechnicsViewModel = ObjectExtension.As<PhototechnicsViewModel.Details>(link.RentCalendars.SingleOrDefault(x => x.Phototechnics.Shortcut == slug));
                return View("Details", data);
            }
            return View("Details");
        }

        private ActionResult Cart(RouteEntity entity)
        {
            if (entity is Photoshop)
            {
                this.SetTheme(entity);

                var viewdata = new CartViewData
                {
                    Menu = ObjectExtension.As<IList<MenuItemViewModel>>(_context.AbstractMenuItem.Where(x => x.RouteID == entity.ID)),
                    Route = ObjectExtension.As<PhotoshopViewModel.Summary>(entity),
                    Categorieses = ObjectExtension.As<IList<CategoryViewModel>>(_context.Categories.Include(x => x.Brands).ToList())
                };

                var cookie = Request.Cookies.Get("shoppingCart");

                string products = string.Empty;

                if (cookie != null)
                    products = HttpUtility.UrlDecode(cookie.Value);

                var serializer = new JavaScriptSerializer();

                if (products == null)
                    viewdata.Orders = new List<OrderLineViewModel>();
                else
                {
                    var data = serializer.Deserialize<IList<PhototechnicsOrderLine>>(products);

                    if (data == null)
                        viewdata.Orders = new List<OrderLineViewModel>();
                    else
                    {
                        var ids = data.Select(x => x.PricePositionID).ToList();

                        var query = (entity as Photoshop).Positions.Where(x => ids.Contains(x.ID));

                        viewdata.Orders = ObjectExtension.As<IList<OrderLineViewModel>>(query.ToList());
                    }
                }
                return View("Cart", viewdata);
            }
            return View("Cart");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }

    }

    /*public class OrderViewModel
    {
        public ICollection<OrderLine> OrderLines { get; set; }
    }*/
}