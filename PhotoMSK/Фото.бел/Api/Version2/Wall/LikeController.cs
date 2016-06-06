using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;

namespace Fotobel.Api.Version2.Wall
{
    public class LikeController : ApiController
    {
        readonly AppContext _context = new AppContext();
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Like/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Like
        [Authorize]
        [HttpPost]
        public LikeStatusViewModel Post([FromBody]LikeViewModel value)
        {
            var accountid = User.Identity.GetUserId();
            var news = _context.Newses.Find(value.PostID);

            if (news == null)
                throw new KeyNotFoundException();

            news.AddLike(accountid, value.LikeType);

            _context.SaveChanges();

            return new LikeStatusViewModel { Likes = news.LikeStatus.Likes, Dislikes = news.LikeStatus.Dislikes, Opinion = value.LikeType };
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete(int id)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);

        }
    }

    public class LikeViewModel
    {
        public Guid PostID { get; set; }
        public LikeType LikeType { get; set; }
    }

    public class LikeStatusViewModel
    {
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public LikeType Opinion { get; set; }
    }
}
