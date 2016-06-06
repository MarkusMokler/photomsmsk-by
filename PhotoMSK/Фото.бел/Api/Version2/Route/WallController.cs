using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Interfaces;

namespace Fotobel.Api.Version2
{
    public class WallController : ApiController
    {
        readonly AppContext _context = new AppContext();
        private readonly IWallService _wallService;

        public WallController(IWallService wallService)
        {
            _wallService = wallService;
        }

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public string Get(int id)
        {
            return "value";
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Post([FromBody]WallModel value)
        {
            var userID = User.Identity.GetUserId();
            var user = _context.Users.Single(x => x.Id == userID);

            var isinrole = user.UserInformation.Roles.Any(x => x.Route.ID == value.Owner && x.AccessStatus == AccessStatus.Owner);

            if (!isinrole)
                return Unauthorized();

            var route = _context.Routes.Single(x => x.ID == value.Owner);

            foreach (var attachment in value.Attachments)
            {
                attachment.ID = Guid.NewGuid();
            }

            var wallid = Guid.NewGuid();
            var wallpost = value.Shooting ? new Shooting() : new WallPost();

            if (value.Shooting)
            {
                var shooting = wallpost as Shooting;
                var photographer = route as Photographer;
                if (photographer != null)
                {
                    var list = value.Genres.Take(3).ToList();

                    foreach (
                        var gr in
                            list.Select(genre => (_context.Genres.SingleOrDefault(x => x.Name == genre) ?? new Genre
                            {
                                ID = Guid.NewGuid(),
                                Name = genre
                            })))
                    {
                        shooting.AddGenre(gr);
                        photographer.AddGenre(gr);
                    }
                }
            }

            wallpost.ID = wallid;
            wallpost.Date = DateTime.Now;
            wallpost.Title = string.IsNullOrEmpty(value.Title) ? null : value.Title;
            wallpost.Desctiption = value.Description;
            wallpost.LikeStatus = new LikeStatus { ID = Guid.NewGuid() };
            wallpost.Attacments = value.Attachments;


            var view = new Views
             {
                 ID = Guid.NewGuid(),
                 Date = DateTime.Now,
                 WallPost = wallpost,
                 ParticipationType = ParticipationType.Owner
             };

            foreach (var guid in value.Routes)
            {
                view.WallPost.Referers.Add(new Views
                {
                    ID = Guid.NewGuid(),
                    Date = DateTime.Now,
                    ParticipationType = ParticipationType.СoAuthor,
                    EntityID = guid
                });
            }

            route.AddWallPost(view);
            _context.SaveChanges();

            return Ok(_wallService.GetDetails(wallid));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class WallModel
    {
        public bool Shooting { get; set; }
        public Guid Owner { get; set; }
        public List<Guid> Routes { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public List<Attachment> Attachments;
        public List<string> Genres { get; set; }
    }
}
