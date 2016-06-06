using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Fotobel.Models;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;

namespace Fotobel.Api.Version2
{
    public class PhotorentController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        public IList<PhotorentViewModel.Details> Get()
        {
            return _context.Photorents.As<IList<PhotorentViewModel.Details>>();
        }

        [HttpGet]
        public IList<PhotorentViewModel.Details> Get(string city)
        {
            return _context.Photorents.Where(x => x.City == city).As<IList<PhotorentViewModel.Details>>();
        }
        [HttpGet]
        public PhotorentViewModel.Details GetByShortcut(string shortcut)
        {
            return _context.Photorents.Single(x => x.Shortcut == shortcut).As<PhotorentViewModel.Details>();
        }
        [HttpPost]
        public IHttpActionResult Post(Proxy<PhotoMSK.Data.Models.Routes.Photorent> model)
        {
           

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var id = Guid.NewGuid();
            var entity = new PhotoMSK.Data.Models.Routes.Photorent{ID = id,Raiting = new Raiting(),LikeStatus = new LikeStatus() {ID = Guid.NewGuid()},CurrencyValue = 1};
            model.Patch(entity);

            var uid = User.Identity.GetUserId();
            var currentUser = _context.Users.Find(uid);

            currentUser.UserInformation.Roles.Add(new Role
            {
                ID = Guid.NewGuid(),
                AccessStatus = AccessStatus.Owner,
                Route = entity
            });


            _context.Photorents.Add(entity);
            _context.SaveChanges();
            return Ok(new { id });
        }

        [HttpPut]
        public IHttpActionResult Put(string shortcut, Proxy<PhotoMSK.Data.Models.Routes.Photorent> model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var route = _context.Photorents.First(x=>x.Shortcut==shortcut);
            model.Patch(route);
            _context.SaveChanges();
            return Ok(model);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                this._context.Dispose();
            base.Dispose(disposing);
        }
    }

}
