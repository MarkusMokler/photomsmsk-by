using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security;
using System.Web.Http;
using Fotobel.Classes;
using Fotobel.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;
using PHS = PhotoMSK.Data.Models.Routes.Photostudio;

namespace Fotobel.Api.Version2
{
    [Error]
    public class PhotostudioController : ApiController
    {
        readonly Lazy<AppContext> _context = new Lazy<AppContext>(() => new AppContext());
        [HttpGet]
        public PageView<PhotostudioViewModel.Summary> Get(int from = 0, int pageSize = 10)
        {
            var query = _context.Value.Photostudios;

            var elements = query.
                Where(x => x.Active)
                .OrderByDescending(x => x.Raiting.Total)
                .Skip(from)
                .Take(pageSize).ToList();

            foreach (var element in elements)
            {
                element.HallCount = element.Halls.Count;
            }

            return new PageView<PhotostudioViewModel.Summary>()
            {
                Elements = elements.As<IList<PhotostudioViewModel.Summary>>(),
                ItemsCount = _context.Value.Photostudios.Count(),
                PageSize = pageSize
            };
        }
        [HttpGet]
        public PhotostudioViewModel.Detaills Get(string id)
        {
            return _context.Value.Photostudios.
                Single(x => x.Shortcut == id && x.Active)
                .As<PhotostudioViewModel.Detaills>();
        }

        [HttpPost]
        [Message("Фотостудия создана")]
        public IHttpActionResult Post(Proxy<PHS> model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            if (_context.Value.Routes.Single(x => x.Shortcut.Equals(model.Model.Shortcut)) != null)
            {
                throw new ValidationException("Короткая ссылка уже занята", MessageCodes.OkAction);
            }

            var photostudio = new PHS { Raiting = new Raiting(), LikeStatus = new LikeStatus() { ID = Guid.NewGuid() } };
            model.Patch(photostudio);

            //var uid = User.Identity.GetUserId();
            var currentUser = _context.Value.Users.FirstOrDefault(x => x.UserName == this.User.Identity.Name);

            currentUser.UserInformation.Roles.Add(new Role
            {
                ID = Guid.NewGuid(),
                AccessStatus = AccessStatus.Owner,
                Route = photostudio
            });

            var id = Guid.NewGuid();
            photostudio.ID = id;
            _context.Value.Photostudios.Add(photostudio);
            _context.Value.SaveChanges();
            return Ok(new { id });
        }
        [Authorize]
        [HttpPut]
        public IHttpActionResult Put(Guid id, [FromBody]Proxy<PHS> model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var route = _context.Value.Photostudios.Find(id);
            model.Patch(route);
            route.HallCount = route.Halls.Count();
            _context.Value.SaveChanges();
            return Ok(model);
        }
        [Authorize]
        [HttpDelete]
        [Message("Студия удалена")]
        public IHttpActionResult Delete(Guid id)
        {
            var photostudio = _context.Value.Photostudios.Single(x => x.ID == id);
            var iid = User.GetUserInformationID();
            if (!photostudio.Participants.Any(x => x.UserInformation.ID == iid && x.AccessStatus == AccessStatus.Owner))
                throw new SecurityException("Not Alowed for current user");

            if (photostudio.LikeStatus != null)
                _context.Value.Entry(photostudio.LikeStatus).State = EntityState.Deleted;

            if (photostudio.Raiting != null)
                _context.Value.Entry(photostudio.Raiting).State = EntityState.Deleted;

            if (photostudio.Participants != null)
            {
                foreach (var participant in photostudio.Participants.ToList())
                {
                    _context.Value.Entry(participant).State = EntityState.Deleted;
                }
            }

            if (photostudio.Phones != null)
            {

                foreach (var routesPhone in photostudio.Phones.ToList())
                {
                    _context.Value.Entry(routesPhone).State = EntityState.Deleted;
                }
            }

            if (photostudio.Photos != null)
            {

                foreach (var routesPhone in photostudio.Photos.ToList())
                {
                    _context.Value.Entry(routesPhone).State = EntityState.Deleted;
                }
            }
            if (photostudio.Wall != null)
            {

                foreach (var routesPhone in photostudio.Wall.ToList())
                {
                    _context.Value.Entry(routesPhone).State = EntityState.Deleted;
                }
            }
            if (photostudio.Halls != null)
            {

                foreach (var hall in photostudio.Halls.ToList())
                {
                    _context.Value.Entry(hall).State = EntityState.Deleted;
                }
            }
            _context.Value.Entry(photostudio).State = EntityState.Deleted;
            _context.Value.SaveChanges();
            return Ok();
        }

    }
}
