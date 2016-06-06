using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Menu;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;

namespace Fotobel.Api.Version2
{
    public class RouteMenuSearchController : ApiController
    {
        AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(string name)
        {
            var menu = _context.RouteMenus.Where(x => x.Name.Contains(name)).ToList().As<List<MenuItemViewModel>>();
            return Ok(menu);
        }
    }
}
