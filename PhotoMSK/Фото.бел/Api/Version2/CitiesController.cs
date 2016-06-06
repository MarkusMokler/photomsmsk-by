using PhotoMSK.Data;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Fotobel.Api.Version2
{
    public class CitiesController : ApiController
    {
        private readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get()
        {
            var groups = _context.Photographers.GroupBy(p => p.City).Select(g => new { Name = g.Key, Count = g.Count() });
            var cities = groups.Where(x => x.Count >= 200).ToList();
            var result = cities.ToDictionary(city => city.Name, city => city.Count);

            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            var photographers = _context.Photographers;
            var query = photographers.Where(x => x.ID == id);

            return Ok(query.As<IList<PhotographerViewModel.Details>>());
        }
    }
}