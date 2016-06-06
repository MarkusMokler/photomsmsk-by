using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using Fotobel.Classes;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.RoutePageLayouts;

namespace Fotobel.Api.Version2.Route
{
    public class PageController : ApiController
    {
        readonly AppContext _context = new AppContext();
        [HttpGet]
        public IHttpActionResult GetRP(string shortcut, string url = "")
        {
            RoutepageLayout rpLayout = null;
            if (!string.IsNullOrEmpty(url) && !string.IsNullOrWhiteSpace(url))
            {
                rpLayout = _context.BasePages.Where(x => x.Route.Shortcut == shortcut).Single(x => x.Slug == url).PageLayout;
            }
            else
            {
                var main = _context.Routes.Single(x => x.Shortcut == shortcut).MainPage;
                if (main != null)
                {
                    rpLayout = _context.RoutePageLayouts.Find(main.PageLayout.ID);
                }
            }

            if (rpLayout == null)
            {
                var rpL = _context.RoutePageLayouts.Find(_context.Routes.Single(x => x.Shortcut.Equals(shortcut)).DefaultRoutePageLayoutID);
                return Ok(rpL.As<RoutePageLayoutsViewModel.Details>());
            }

            return Ok(rpLayout.As<RoutePageLayoutsViewModel.Details>());
        }

        [HttpPut]
        [Message("Страница установлена по умолчанию")]
        public IHttpActionResult PostDefaultPage(string shortcut, TextPageViewModel.Summary page)
        {
            var route = _context.Routes.Single(x => x.Shortcut.Equals(shortcut));
            route.MainPage = _context.BasePages.Find(page.Id);
            route.MainPageID = page.Id;
            _context.SaveChanges();
            return Ok();
        }
    }
}
