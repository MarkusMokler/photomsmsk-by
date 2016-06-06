using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Fotobel.Models;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;

namespace Fotobel.Api.Version2
{
    public class MasterclassController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(int from = 0, int pageSize = 3)
        {
            var query = _context.Masterclasses;

            var elements = query.
                OrderBy(x=>x.Pro).
                ThenBy(x => x.Verified).
                ThenByDescending(x => x.Raiting.Total).
                Skip(from).
                Take(pageSize).ToList();

            var result = new PageView<MasterclassViewModel.Summary>()
            {
                Elements = elements.As<IList<MasterclassViewModel.Summary>>(),
                ItemsCount = _context.Masterclasses.Count(),
                PageSize = pageSize
            };

            return Ok(result);
        }

        public IHttpActionResult GetById([FromUri]Guid id) //get getails
        {
            var result = new PageView<MasterclassViewModel.Details>()
            {
                Elements = _context.Masterclasses.Where(a => a.ID == id).As<IList<MasterclassViewModel.Details>>()
            };
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult Post(Proxy<Masterclass> model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
      
            var id = Guid.NewGuid();

            var entity = new Masterclass { ID = id, Raiting = new Raiting(), LikeStatus = new LikeStatus { ID = Guid.NewGuid() } };
            
            model.Patch(entity);

            entity.Start = new DateTime(model.Model.StartDay.Year, model.Model.StartDay.Month, model.Model.StartDay.Day, model.Model.Start.Hour, model.Model.Start.Minute, 0);
            entity.End = new DateTime(model.Model.StartDay.Year, model.Model.StartDay.Month, model.Model.StartDay.Day, model.Model.End.Hour, model.Model.End.Minute, 0);

            var uid = User.Identity.GetUserId();
            var currentUser = _context.Users.Find(uid);

            currentUser.UserInformation.Roles.Add(new Role
            {
                ID = Guid.NewGuid(),
                AccessStatus = AccessStatus.Owner,
                Route = entity
            });

            _context.Masterclasses.Add(entity);
            _context.SaveChanges();
            return Ok(new { id });
        }

        public IHttpActionResult Put(Guid id, Proxy<Masterclass> model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var route = _context.Masterclasses.Find(id);
            model.Patch(route);
            _context.SaveChanges();
            return Ok(model);
        }

        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
