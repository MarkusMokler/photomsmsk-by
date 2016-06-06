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
using PhotoMSK.ViewModels.RoutePageLayouts;
using Page = PhotoMSK.Data.Models.Routes.Page;

namespace Fotobel.Api.Version2
{
    public class PageController : ApiController
    {
        private readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(int from = 0, int pageSize = 12)
        {
            var query = _context.Pages;
            var elements = query.
                OrderBy(x => x.Pro)
                .ThenBy(x => x.Verified)
                .ThenByDescending(x => x.Raiting.Total)
                .Skip(from)
                .Take(pageSize).ToList();

            var res = new PageView<PageViewModel.Summary>()
            {
                Elements = elements.As<IList<PageViewModel.Summary>>(),
                ItemsCount = _context.Pages.Count(),
                PageSize = pageSize
            }
            ;
            return Ok(res);
        }

        [HttpPost]
        public IHttpActionResult Post(Proxy<Page> model)
        {
            var id = Guid.NewGuid();

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);



            var entity = new Page { ID = id, Raiting = new Raiting(), LikeStatus = new LikeStatus { ID = Guid.NewGuid() } };
            model.Patch(entity);

            var uid = User.Identity.GetUserId();
            var currentUser = _context.Users.Find(uid);

            currentUser.UserInformation.Roles.Add(new Role
            {
                ID = Guid.NewGuid(),
                AccessStatus = AccessStatus.Owner,
                Route = entity
            });

            _context.Pages.Add(entity);
            _context.SaveChanges();
            return Ok(new { id });
        }

        [HttpPut]
        public IHttpActionResult Put(Guid id, Proxy<Page> model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var route = _context.Pages.Find(id);
            model.Patch(route);
            _context.SaveChanges();
            return Ok(model.As<PageViewModel>());
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
