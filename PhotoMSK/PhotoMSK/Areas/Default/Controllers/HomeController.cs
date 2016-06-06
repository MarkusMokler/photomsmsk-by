using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Lucene.Net.Index;
using Microsoft.AspNet.Identity;
using PhotoMSK.Areas.Default.ViewData.Home;
using PhotoMSK.Areas.Default.ViewData.Photoshop;
using PhotoMSK.Classes;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Extensiolns;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Routes;
using IndexViewData = PhotoMSK.Areas.Default.ViewData.Home.IndexViewData;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class HomeController : RouteController
    {
        private readonly ILuceneProvider _provider;
        private readonly IWallService _wallService;
        private readonly IMasterclassService _masterclassService;

        public HomeController(ILuceneProvider provider, IWallService wallService, IPhototechnicsService phototechnicsService
            , IMasterclassService masterclassService)
            : base(phototechnicsService)
        {
            _provider = provider;
            _wallService = wallService;
            _masterclassService = masterclassService;
        }
        public ActionResult Theme(string id = "")
        {
            if (!Context.Routes.Any(x => x.Shortcut == id))
            {
                return HttpNotFound();
            }

            var elem = Context.Routes.Single(x => x.Shortcut == id);
            elem.AddView();
            Context.SaveChanges();

            this.SetTheme(elem, true);
            return View(elem);
        }
        public ActionResult Index(string id = "")
        {
            if (!Context.Routes.Any(x => x.Shortcut == id && x.Active))
            {
                var data = new IndexViewData();
                var mainwall = _wallService.GetMainWall();
                data.MainWall = mainwall.ToList();
                data.Masterclasses = _masterclassService.GetNearestMasterclasses();

                return View(data);
            }

            var elem = Context.Routes
                .Include(x => x.Raiting)
                .Single(x => x.Shortcut == id && x.Active);

            this.SetRoute(elem);

            elem.AddView();
            Context.SaveChanges();

            return View(elem);
        }
        public ActionResult Index2(string id = "")
        {
            if (!Context.Routes.Any(x => x.Shortcut == id))
            {
                var data = new IndexViewData();
                var mainwall = _wallService.GetMainWall();
                data.MainWall = mainwall.ToList();

                return View(data);
            }

            var elem = Context.Routes.Single(x => x.Shortcut == id);
            elem.AddView();
            Context.SaveChanges();
            return View(elem);
        }
        public ActionResult Reindex()
        {
            IndexWriter writer = _provider.Writer;
            List<RouteEntity> routes = Context.Routes.ToList();

            foreach (RouteEntity routeEntity in routes)
            {
                writer.Write(routeEntity);
            }

            writer.Commit();

            return RedirectToAction("Index");
        }
        public ActionResult AboutPhotomsk()
        {
            return View();
        }
        public async Task<ActionResult> News(string id)
        {
            var elem = await Context.Routes.SingleAsync(x => x.Shortcut == id);
            elem.AddView();

            this.SetTheme(elem);
            await Context.SaveChangesAsync();

            this.SetTheme(elem);

            var phs = elem as Photoshop;
            if (phs != null)
            {
                var model = new DetailsViewData
                 {
                     RouteDetails = phs.As<PhotoshopViewModel.Details>(),
                     Categorieses = Context.Categories.ToList().As<IList<CategoryViewModel>>()
                 };
                return View("News", "_Layout", model);
            }
            var phr = elem as Photorent;
            if (phr != null)
            {
                var model = phs.As<PhotorentViewModel.Details>();
                //   model.Categorieses = _context.Categories.ToList().As<IList<CategoryViewModel>>();
                return View("News", "_Layout", model);
            }
            return HttpNotFound();
        }
        public async Task<ActionResult> Newswl(string whitelabel)
        {
            var elem = await Context.Routes.SingleAsync(x => x.Domain == whitelabel);
            elem.AddView();

            this.SetTheme(elem);
            await Context.SaveChangesAsync();

            this.SetTheme(elem);

            var phs = elem as Photoshop;

            if (phs != null)
            {

                var model = new WhiteLabelNewsViewData()
                {
                    Route = phs.As<PhotoshopViewModel.Summary>(),
                    Categorieses = Context.Categories.ToList().As<IList<CategoryViewModel>>(),
                    Wall = _wallService.GetWallForRoute(phs.ID).ToList()
                };
                return View("News", model);
            }
            return HttpNotFound();
        }


        [Authorize]
        [ChildActionOnly]
        public ActionResult UserMenu()
        {
            string id = User.Identity.GetUserId();
            User user = Context.Users.Find(id);

            return View(user);
        }
    }
}