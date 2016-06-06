using System.Web.Http;
using Fotobel.Hubs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using PhotoMSK.Data;

namespace Fotobel.Api.Services
{
    [System.Web.Http.Authorize]
    public class NumberController : ApiController
    {
        AppContext _context = new AppContext();

        [System.Web.Http.Authorize]
        public IHttpActionResult Post([FromBody]PhoneRequest request)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<CallerHub>();
            var uid = User.Identity.GetUserId();

            hubContext.Clients.User(uid).showInfoByNumber(request.Number.Replace("+", string.Empty));
            return Ok();
        }

        public class PhoneRequest
        {
            public string Number { get; set; }
        }
    }
}
