using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;

namespace Fotobel.Api.Version2.Route
{
    public class SubscriptionController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Post(JObject jObject)
        {
            var userID = User.Identity.GetUserId();
            var studio = await _context.Routes.FindAsync(jObject.GetValue("RouteID").ToObject<Guid>());

            if (_context.Participants.Any(x => x.UserInformation.User.Id == userID && x.Route.ID == studio.ID))
                return Ok();

            _context.Participants.Add(new Role
            {
                ID = Guid.NewGuid(),
                AccessStatus = AccessStatus.User,
                Route = studio,
                UserInformation = _context.Users.Find(userID).UserInformation
            });

            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri]Guid id)
        {
            var userID = User.Identity.GetUserId();
            var route = _context.Routes.Find(id);

            var role = _context.Participants.FirstOrDefault(x => x.UserInformation.User.Id == userID && x.Route.ID == route.ID);

            if (role == null)
                return Ok();

            _context.Entry(role).State = EntityState.Deleted;
            _context.SaveChanges();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
