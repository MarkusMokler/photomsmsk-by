using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Attachments;

namespace Fotobel.Api.Version2
{
    public class RoutePhotosController : ApiController
    {
        readonly AppContext _context = new AppContext();
        [HttpPut]
        public IHttpActionResult Put(string shortcut, [FromBody]ICollection<Photo> model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var route = _context.Routes.First(x => x.Shortcut == shortcut);
            foreach (var photo in model)
            {
                photo.ID = Guid.NewGuid();
                route.Photos.Add(photo);
            }
            _context.SaveChanges();
            return Ok(model);
        }
        [HttpPost]
        public IHttpActionResult Post(JObject jObject)
        {
            var id = jObject.GetValue("id").ToObject<Guid>();
            var photos = jObject.GetValue("photos").ToObject<List<Photo>>();

            var photostudio = _context.Routes.Find(id);

            if (photostudio == null)
                return NotFound();

            foreach (var photo in photos)
            {
                photo.ID = Guid.NewGuid();
                photo.UploadDate = DateTime.Now;
                photostudio.Photos.Add(photo);
            }

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri]Guid id)
        {
            var photo = _context.Photos.Find(id);
            _context.Entry(photo).State = EntityState.Deleted;
            _context.SaveChanges();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }

        public class RequestObject
        {
            public Guid ID { get; set; }
            public string Url { get; set; }
            public string Type { get; set; }
        }
    }
}
