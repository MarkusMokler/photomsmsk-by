using System;
using System.Linq;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;

namespace Fotobel.Api.Version2.Route
{
    public class DeleteRequestController : ApiController
    {
        private readonly AppContext _db = new AppContext();

        [HttpPost]
        public IHttpActionResult SaveReason(string shortcut, [FromBody]DeleteRouteRequest deleteRouteRequest)
        {
            var route = _db.Routes.Single(x => x.Shortcut == shortcut);

            deleteRouteRequest.Date = DateTime.Now;
            deleteRouteRequest.ID = Guid.NewGuid();
            deleteRouteRequest.Route = route;
            deleteRouteRequest.RouteID = route.ID;

            _db.DeleteRouteRequests.Add(deleteRouteRequest);
            route.Active = false;
            //_db.SaveChanges();

            return Ok(new { message = "Роут удален" });
        }
    }
}