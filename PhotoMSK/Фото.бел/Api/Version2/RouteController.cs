using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Http;
using Fotobel.Models;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2
{

    public class RouteController : ApiController
    {
        readonly Lazy<AppContext> _context = new Lazy<AppContext>(() => new AppContext());

        [System.Web.Mvc.HttpGet]
        public IHttpActionResult Get(string id)
        {
            var route = _context.Value.Routes.SingleOrDefault(x => x.Shortcut == id);
            if (route != null)
                return Ok(new { RouteType = ObjectContext.GetObjectType(route.GetType()).Name });
            return BadRequest();
        }

    }
}