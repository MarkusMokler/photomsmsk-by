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
using PhotoMSK.ViewModels.Routes;

namespace Fotobel.Api.Version2
{
    public class PhotoshopController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        public IList<PhotoshopViewModel.Summary> Get()
        {
            return _context.Photostudios.As<IList<PhotoshopViewModel.Summary>>();
        }

        [HttpGet]
        public PhotoshopViewModel.Summary GetByShortcut(string shortcut)
        {
            return _context.Photoshops.Single(x => x.Shortcut == shortcut).As<PhotoshopViewModel.Summary>();
        }

        [HttpGet]
        public IList<PhotoshopViewModel.Summary> Get(string city)
        {
            return _context.Photostudios.Where(x => x.City == city).As<IList<PhotoshopViewModel.Summary>>();
        }

        [HttpPost]
        public IHttpActionResult Post(Proxy<PhotoMSK.Data.Models.Routes.Photoshop> model)
        {

            var id = Guid.NewGuid();

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);


            var entity = new PhotoMSK.Data.Models.Routes.Photoshop { ID = id, Raiting = new Raiting(), LikeStatus = new LikeStatus { ID = Guid.NewGuid() } };
            model.Patch(entity);

            var uid = User.Identity.GetUserId();
            var currentUser = _context.Users.Find(uid);

            currentUser.UserInformation.Roles.Add(new Role
            {
                ID = Guid.NewGuid(),
                AccessStatus = AccessStatus.Owner,
                Route = entity
            });


            _context.Photoshops.Add(entity);
            _context.SaveChanges();
            return Ok(new { id });
        }

        [HttpPut]
        public IHttpActionResult PutByShortcut(string shortcut, Proxy<PhotoMSK.Data.Models.Routes.Photoshop> model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var route = _context.Photoshops.First(x=>x.Shortcut == shortcut);
            model.Patch(route);
            _context.SaveChanges();
            return Ok(model);
        }

        [HttpPut]
        public IHttpActionResult Put(Guid id, Proxy<PhotoMSK.Data.Models.Routes.Photoshop> model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var route = _context.Photoshops.Find(id);
            model.Patch(route);
            _context.SaveChanges();
            return Ok(model);
        }

        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                this._context.Dispose();
            base.Dispose(disposing);
        }
    }
}
