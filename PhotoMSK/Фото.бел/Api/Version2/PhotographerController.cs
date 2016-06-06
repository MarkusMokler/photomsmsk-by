using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security;
using System.Web.Http;
using Fotobel.Classes;
using Fotobel.Models;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Routes;

namespace Fotobel.Api.Version2
{
    public class PhotographerController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(int from = 0, int pageSize = 20, string city = null)
        {
            var query = _context.Photographers;
            var count = query.Count(x => x.City == city);  // Number of photographers
            var elements = new List<Photographer>();

            if (city == null)
            {
                elements = query.
                OrderBy(x => x.Pro)
                .ThenBy(x => x.Verified)
                .ThenByDescending(x => x.Raiting.Total)
                .Skip(from)
                .Take(pageSize).ToList();
                count = _context.Photographers.Count();
            }
            else
            {
                elements = query
                .Where(x => x.City == city)
                .OrderBy(x => x.Pro)
                .ThenBy(x => x.Verified)
                .ThenByDescending(x => x.Raiting.Total)
                .Skip(from)
                .Take(pageSize).ToList();
            }

            var res = new PageView<PhotographerViewModel.Summary>()
            {
                Elements = elements.As<IList<PhotographerViewModel.Summary>>(),
                ItemsCount = count,
                PageSize = pageSize
            }
            ;
            return Ok(res);
        }

        [HttpGet]
        public IHttpActionResult Get(string shortcut)
        {
            var query = _context.Photographers;

            var res = query.Single(x => x.Shortcut == shortcut);
            return Ok(res.As<PhotographerViewModel.Details>());

        }

        [HttpPut]
        public IHttpActionResult Put(Guid id, Proxy<Photographer> model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var route = _context.Photographers.Find(id);
            model.Patch(route);
            _context.SaveChanges();
            return Ok(model);
        }

        [HttpPost]
        public IHttpActionResult Post(Proxy<Photographer> model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var photographer = new PhotoMSK.Data.Models.Routes.Photographer { Raiting = new Raiting(), LikeStatus = new LikeStatus() { ID = Guid.NewGuid() } };
            model.Patch(photographer);

            var id = Guid.NewGuid();
            photographer.ID = id;
            //var uid = User.Identity.GetUserId();
            var currentUser = _context.Users.FirstOrDefault(x => x.UserName == this.User.Identity.Name);

            currentUser.UserInformation.Roles.Add(new Role
            {
                ID = Guid.NewGuid(),
                AccessStatus = AccessStatus.Owner,
                Route = photographer
            });

            _context.Photographers.Add(photographer);
            _context.SaveChanges();
            return Ok(new { id });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            var photographer = _context.Photographers.Single(x => x.ID == id);
            var userId = User.GetUserInformationID();
            if (!photographer.Participants.Any(x => x.UserInformation.ID == userId && x.AccessStatus == AccessStatus.Owner))
                throw new SecurityException("Не достаточно прав для выполнения данной операции");

            if (photographer.LikeStatus != null)
                _context.Entry(photographer.LikeStatus).State = EntityState.Deleted;

            if (photographer.Raiting != null)
                _context.Entry(photographer.Raiting).State = EntityState.Deleted;

            if (photographer.Participants != null)
            {
                foreach (var participant in photographer.Participants.ToList())
                {
                    _context.Entry(participant).State = EntityState.Deleted;
                }
            }

            if (photographer.Phones != null)
            {
                foreach (var routesPhone in photographer.Phones.ToList())
                {
                    _context.Entry(routesPhone).State = EntityState.Deleted;
                }
            }

            if (photographer.Photos != null)
            {
                foreach (var routesPhone in photographer.Photos.ToList())
                {
                    _context.Entry(routesPhone).State = EntityState.Deleted;
                }
            }
            if (photographer.Wall != null)
            {
                foreach (var routesPhone in photographer.Wall.ToList())
                {
                    _context.Entry(routesPhone).State = EntityState.Deleted;
                }
            }

            _context.Entry(photographer).State = EntityState.Deleted;
            _context.SaveChanges();
            return Ok();
        }
    }
}
