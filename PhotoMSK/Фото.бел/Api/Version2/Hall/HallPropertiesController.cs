using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2
{
    public class HallPropertiesController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(Guid hallId)
        {
            var incl =
                _context.Halls.Where(x => x.ID == hallId)
                    .SelectMany(x => x.HallProperties);

            var ex = _context.HallProperties.Except(incl);
            return Ok(
                new {incl = incl.ToList()
                    .As<IList<HallPropertyViewModel.Summary>>(),
                    ex = ex.ToList()
                    .As<IList<HallPropertyViewModel.Summary>>()
            });
        }
        
        /// <summary>
        /// for autocomplete, return list where x.name.startwith(search)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public IList<HallPropertyViewModel.Summary> Get(string search)
        {
            return _context.HallProperties.Where(x => x.Name.StartsWith(search)).ToList().As<IList<HallPropertyViewModel.Summary>>();
        }

        [HttpPut]
        public IHttpActionResult Put(PropertyHall model)
        {
            var pr = _context.HallProperties.Find(model.HallPropertyID);
            var hall = _context.Halls.Find(model.HallID);

            hall.HallProperties.Add(pr);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Post(CreatePropertyModel model)
        {
            var newProperty = new HallProperty
            {
                ID = Guid.NewGuid(),
                Name = model.Property
            };
            
            var hall = _context.Halls.Find(model.HallID);

            hall.HallProperties.Add(newProperty);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri]Guid hallID, [FromUri] Guid hallPropertyID)
        {
            var pr = _context.HallProperties.Find(hallPropertyID);
            var hall = _context.Halls.Find(hallID);

            hall.HallProperties.Remove(pr);
            _context.SaveChanges();

            return Ok();
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

    public class PropertyHall
    {
        public Guid HallID { get; set; }
        public Guid HallPropertyID { get; set; }
    }

    public class CreatePropertyModel
    {
        public Guid HallID { get; set; }
        public string Property { get; set; }
    }
}
