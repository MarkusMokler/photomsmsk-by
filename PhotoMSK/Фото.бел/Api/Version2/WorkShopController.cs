using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhotoMSK.Data;

namespace Fotobel.Api.Version2
{
    public class WorkShopController : ApiController
    {

        private readonly AppContext _context = new AppContext();
        [HttpGet]
        public IHttpActionResult Get(string shortcut)
        {
            var routes = _context.Routes.Single(x => x.Shortcut == shortcut).FriendlyRoute;
            return Ok(routes);
        }
    }
}
