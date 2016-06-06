using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Fotobel.Models;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Routes;

namespace Fotobel.Api.Version2
{
    public class ReviewsController : ApiController
    {
        private AppContext _db = new AppContext();
        // GET: Reviews
        [HttpGet]
        public IHttpActionResult Get(string shortcut)
        {
            var reviews = _db.RouteReviews.Where(x=>x.Route.Shortcut==shortcut).OrderBy(x=>x.Date).Take(10).ToList();
            return Ok(reviews.As<IList<RouteReviewViewModel.Summary>>());
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(Guid userID, ReviewCreate review)
        {
            RouteReview routeReview = new RouteReview()
            {
                ID = Guid.NewGuid(),
                User = _db.UserInformations.Find(userID),
                LikeStatus = new LikeStatus()
                {
                    ID = Guid.NewGuid()
                },
                Title = review.Title,
                Description = review.Description,
                Route = _db.Routes.Single(x=>x.Shortcut == review.RouteShortcut),
                GoodComment = review.GoodComment,
                BadComment = review.BadComment,
                Date = DateTime.Now
            };

            _db.RouteReviews.Add(routeReview);
            _db.SaveChanges();
            return Ok();
        }
    }

    public class ReviewCreate
    {
        public string Title;
        public string Description;
        public string GoodComment;
        public string BadComment;
        public string RouteShortcut;
    }
}