using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Fotobel.Models;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Pages;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.RoutePageLayouts;
using UnidecodeSharpFork;

namespace Fotobel.Api.Version2
{
    public class TextPageController : ApiController
    {
        private readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult GetPages(string shortcut)
        {
            var list = _context.BasePages.Where(x => x.Route.Shortcut == shortcut)
                 .Take(10)
                 .As<IList<TextPageViewModel.Summary>>();
            return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult Get(string search)
        {
            var elements = _context.RoutePageLayouts.Where(x => x.Name.Contains(search)).ToList();
            return Ok(elements.As<IList<RoutePageLayoutsViewModel.Named>>());
        }

        [HttpPost]
        public IHttpActionResult Post(string shortcut, [FromBody] Proxy<BasePage> proxy)
        {
            var route = _context.Routes.Single(x => x.Shortcut == shortcut);
            BasePage page;

            switch (proxy.Model.PageType)
            {
                case "textPage":
                    {
                        page = new ContentPage() { ID = Guid.NewGuid() };
                        break;
                    }
                case "landingPage":
                    {
                        page = new LandingPage() { ID = Guid.NewGuid() };
                        break;
                    }
                case "requestPage":
                    {
                        page = new ProjectionPage() { ID = Guid.NewGuid() };
                        break;
                    }
                default:
                    throw new NotSupportedException();
            }


            if (string.IsNullOrEmpty(proxy.Model.Slug))
                proxy.Model.Slug = proxy.Model.Title.Replace("&", "").Replace(" ", "").Unidecode();

            page.PageLayout = _context.RoutePageLayouts.Find(page.PageLayoutID);
            proxy.Patch(page);

            var userid = User.Identity.GetUserId();

            page.UserInformation = _context.UserInformations.SingleOrDefault(x => x.User.Id == userid);
            page.Route = route;
            page.RouteID = route.ID;
            page.PageCategory = _context.PageCategories.Find(page.PageCategoryID);
            _context.BasePages.Add(page);
            _context.SaveChanges();

            return Ok(page.As<TextPageViewModel.Details>());

        }

        [HttpPut]
        public IHttpActionResult Put(Guid id, Proxy<BasePage> proxy)
        {
            var page = _context.BasePages.Find(id);

            proxy.Patch(page);

            var userid = User.Identity.GetUserId();
            page.UserInformation = _context.UserInformations.SingleOrDefault(x => x.User.Id == userid);

            _context.SaveChanges();
            return Ok(proxy.Model);

        }

        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            var page = _context.BasePages.Find(id);
            _context.Entry(page).State = EntityState.Deleted;
            _context.SaveChanges();
            return Ok();
        }

    }
}