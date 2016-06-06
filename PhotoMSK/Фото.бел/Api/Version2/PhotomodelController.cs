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

namespace Fotobel.Api.Version2
{
    public class PhotomodelController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(int from = 0, int pageSize = 20)
        {
            var query = _context.Photomodels;

            var elements = query.
                OrderBy(x => x.Pro)
                .ThenBy(x => x.Verified)
                .ThenByDescending(x => x.Raiting.Total)
                .Skip(from)
                .Take(pageSize).ToList();

            var res = new PageView<PhotomodelViewModel.Summary>()
            {
                Elements = elements.As<IList<PhotomodelViewModel.Summary>>(),
                ItemsCount = _context.Photomodels.Count(),
                PageSize = pageSize
            }
            ;
            return Ok(res);
        }

        public IHttpActionResult GetDetails(string id)
        {
            var query = _context.Photomodels;

            var elements = query.Where(x => x.Shortcut == id).ToList();
            var res = new PageView<PhotomodelViewModel.Details>()
            {
                Elements = elements.As<IList<PhotomodelViewModel.Details>>(),
                ItemsCount = 1,
                PageSize = 1
            }
            ;
            return Ok(res);
        }

        [HttpPost]
        public IHttpActionResult Post(Proxy<Photomodel> model)
        {
            var id = Guid.NewGuid();

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var entity = new Photomodel { ID = id, Raiting = new Raiting(), LikeStatus = new LikeStatus() { ID = Guid.NewGuid() } };
            model.Patch(entity);
            entity.StartYear = DateTime.Now;
            entity.Article = new Article();

            var uid = User.Identity.GetUserId();
            var currentUser = _context.Users.Find(uid);

            currentUser.UserInformation.Roles.Add(new Role
            {
                ID = Guid.NewGuid(),
                AccessStatus = AccessStatus.Owner,
                Route = entity
            });

            _context.Photomodels.Add(entity);
            _context.SaveChanges();
            return Ok(new { id });
        }

        [HttpPut]
        public IHttpActionResult Put(Guid id, Proxy<Photomodel> model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var entity = _context.Routes.Find(id);
            model.Patch(entity);
            _context.SaveChanges();
          
            return Ok(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
