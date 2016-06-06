using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Fotobel.Models;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2.Route
{
    public class TextPageCategoryController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(string shortcut)
        {
            var routeID = _context.Routes.Single(x => x.Shortcut == shortcut).ID;
            return Ok(_context.PageCategories.Where(x => x.RouteID == routeID).ToList().As<IList<PageCategoryViewModel.Summary>>());
        }

        [HttpPost]
        public IHttpActionResult Post(string shortcut, Proxy<PageCategory> model)
        {
            var categoryId = Guid.NewGuid();
            var route = _context.Routes.Single(x => x.Shortcut == shortcut);
            var category = new PageCategory() { ID = categoryId };
            category.Route = route;
            category.RouteID = route.ID;
            model.Patch(category);

            _context.PageCategories.Add(category);
            _context.SaveChanges();
        
            return Ok(_context.PageCategories.Find(categoryId).As<PageCategoryViewModel.Summary>());
        }

        [HttpPut]
        public IHttpActionResult Put(Guid id, Proxy<PageCategory> model)
        {
            var mmodel = _context.PageCategories.Find(id);

            model.Patch(mmodel);
            _context.SaveChanges();
            return Ok(mmodel);
        }

        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            var cat = _context.PageCategories.Find(id);
            if (cat != null)
                _context.Entry(cat).State = EntityState.Deleted;
            _context.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
