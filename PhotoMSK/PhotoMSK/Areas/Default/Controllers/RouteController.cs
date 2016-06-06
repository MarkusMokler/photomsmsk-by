using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Extensiolns;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Routes;

namespace PhotoMSK.Areas.Default.Controllers
{
    public abstract class RouteController : Controller
    {
        protected readonly AppContext Context = new AppContext();
        private IPhototechnicsService _phototechnicsService;

        /// <summary>
        /// Construct new Instance of class
        /// </summary>
        /// <param name="phototechnicsService"></param>
        protected RouteController(IPhototechnicsService phototechnicsService)
        {
            _phototechnicsService = phototechnicsService;
        }

        /// <summary>
        /// Return RouteView
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public ActionResult View(RouteEntity elem)
        {
            return (ActionResult)GetType().GetMethod("View", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { elem.GetType().BaseType }, null).Invoke(this, new object[] { elem });
        }


        protected virtual ActionResult View(Place place)
        {
            return View("Details", place);
        }
        protected virtual ActionResult View(Photomodel model)
        {
            return View("Details", new ViewData.Model.DetailsViewData()
            {
                Item = ObjectExtension.As<PhotomodelViewModel.Details>(model)
            });
        }
        protected virtual ActionResult View(Photographer model)
        {
            var ids = model.Wall.Select(x => x.ID).ToList();
            Context.WallPostAddView(ids);
            Context.SaveChanges();
            return View("Details", new ViewData.Photographer.DetailsViewData()
            {
                Item = ObjectExtension.As<PhotographerViewModel.Details>(model)
            });
        }
        protected virtual ActionResult View(Page phs)
        {
            var ids = phs.Wall.Select(x => x.ID).ToList();
            Context.WallPostAddView(ids);
            Context.SaveChanges();
            return View("Details", new ViewData.Page.DetailsViewData()
            {
                Item = ObjectExtension.As<PublicViewModel.Details>(phs)
            });
        }
        protected virtual ActionResult View(Masterclass model)
        {
            return View("Details",
                new ViewData.Masterclass.DetailsViewData()
            {
                Item = ObjectExtension.As<MasterclassViewModel.Details>(model)
            });
        }
        protected virtual ActionResult View(Photoshop model)
        {
            var vmodel = ObjectExtension.As<PhotoshopViewModel.Details>(model);
            var data = new ViewData.Photoshop.DetailsViewData
            {
                RouteDetails = vmodel,
                Technics = _phototechnicsService.GetPhotoshopTechnics(vmodel, new PageRequest<PricePosition>() { Where = x => x.PhotoshopID == vmodel.ID }),
                Categorieses = ObjectExtension.As<IList<CategoryViewModel>>(Context.Categories.Include(x => x.Brands).ToList())
            };
            return View("Details", data);
        }
        protected virtual ActionResult View(Photorent model)
        {
            var vmodel = ObjectExtension.As<PhotorentViewModel.Details>(model);
            var data = new ViewData.Photorent.DetailsViewData
            {
                RouteDetails = vmodel,
                Categorieses = ObjectExtension.As<IList<CategoryViewModel>>(Context.Categories.ToList()),
                Technics = _phototechnicsService.GetPhotorentTechnics(vmodel, new PageRequest<RentCalendar>() { Where = x => x.PhotorentID == vmodel.ID }),

            };
            return View("Details", data);
        }
        protected virtual ActionResult View(Phototechnics model)
        {
            return View("Details", model);
        }
        protected virtual ActionResult View(Photostudio model)
        {
            return View("Details", new ViewData.Photostudio.DetailsViewData() { Item = ObjectExtension.As<PhotostudioViewModel.Detaills>(model) });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Context.Dispose();
            base.Dispose(disposing);
        }
    }
}