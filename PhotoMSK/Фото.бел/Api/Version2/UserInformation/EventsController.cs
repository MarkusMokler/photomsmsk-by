using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.ViewModels.Event;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2.UserInformation
{
    [Authorize]
    public class EventsController : ApiController
    {
        readonly AppContext _context = new AppContext();
        public IHttpActionResult Get(Guid shortcut)
        {
            var data = User.Identity.GetUserId();
            var routeOwner = _context.UserInformations.First(x => x.User.Id == data);

            var routes = _context.Participants.Where(
                x => x.UserInformation.ID == routeOwner.ID && x.AccessStatus <= AccessStatus.Administrator)
                .Select(x => x.Route.ID);

            var cardUser = _context.UserInformations.First(x => x.ID == shortcut);
            var events = cardUser.Events.ToList().Where(x => x.Calendar == null || routes.Contains(x.Calendar.RouteEntity.ID)).ToList();
            return Ok(events.OrderByDescending(x => x.CreaTime).As<IList<AdminEventViewModel>>());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
