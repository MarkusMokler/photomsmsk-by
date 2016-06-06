using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Fotobel.Classes;
using Newtonsoft.Json.Linq;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Attachments;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2
{
    public class HallPhotosController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpPut]
        [Message("Фото добавлены")]
        public IHttpActionResult Put(Guid id, [FromBody]List<PhotoViewModel.Summary> model)
        {
            var route = _context.Halls.Find(id);
            foreach (var photo in model)
            {
                photo.ID = Guid.NewGuid();
                route.Photos.Add(new Photo()
                {
                    ID = Guid.NewGuid(),
                    Description = photo.Description,
                    Title = photo.Title,
                    FileName = photo.FileName,
                    Url = photo.Url,
                    UploadDate = DateTime.Now
                });
            }
            _context.SaveChanges();
            return Ok(route.As<HallViewModel.Details>());
        }

        [HttpPost]
        public IHttpActionResult Post(JObject jObject)
        {
            var id = jObject.GetValue("id").ToObject<Guid>();
            var photos = jObject.GetValue("photos").ToObject<List<Photo>>();

            var hall = _context.Halls.Find(id);

            if (hall == null)
                return NotFound();

            foreach (var photo in photos)
            {
                photo.ID = Guid.NewGuid();
                photo.UploadDate = DateTime.Now;
                hall.Photos.Add(photo);
            }

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
