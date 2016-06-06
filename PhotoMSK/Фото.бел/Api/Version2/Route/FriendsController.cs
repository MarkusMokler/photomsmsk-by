using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Routes;

namespace Fotobel.Api.Version2.Route
{
    public class FriendsController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(string shortcut)
        {
            return Ok(_context.Friends.Where(x => x.BaseRoute.Shortcut == shortcut).ToList().As<IList<FriendshipViewModel.Summary>>());
        }


        [HttpPost]
        public IHttpActionResult Get(string shortcut, FriendshipModel model)
        {
            var f = new Friendship()
            {
                ID = Guid.NewGuid(),
                BaseRoute = _context.Routes.Single(x => x.Shortcut == shortcut),
                ChildRouteID = model.ChildRouteID,
                FriendshipType = model.FriendshipType,
            };
            _context.Friends.Add(f);
            _context.SaveChanges();
            return Ok(true);
        }


    }

    public class FriendshipModel
    {
        public Guid ChildRouteID { get; set; }
        public FriendshipType FriendshipType { get; set; }
    }
}
