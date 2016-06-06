using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Comments;
using PhotoMSK.ViewModels;

namespace Fotobel.Api.Version2.Wall
{
    public class WallCommentController : ApiController
    {
        AppContext _context = new AppContext();

        [Authorize]
        [HttpPost]
        public IHttpActionResult Post([FromBody] CommentViewModel comment)
        {
            var post = _context.Newses.SingleOrDefault(x => x.ID == comment.ID);
            if (post == null)
                return NotFound();

            var uid = this.User.Identity.GetUserId();
            var info = _context.UserInformations.Single(x => x.User.Id == uid);
            Comment c = new Comment() { ID = Guid.NewGuid(), Text = comment.Text, User = info, CreationTime = DateTime.Now };
            if (comment.UserInformationID != Guid.Empty)
                c.AnsweredUser = _context.UserInformations.Single(x => x.ID == comment.UserInformationID);

            post.AddComment(c);
            _context.SaveChanges();
            return Ok(Mapper.Map<CommentViewModel>(c));

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
}
