using System;
using PhotoMSK.Data;
using PhotoMSK.ViewModels.Attachments;
using PhotoMSK.ViewModels.Realisation;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Fotobel.Classes;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Attachments;

namespace Fotobel.Api.Version2
{
    public class PhotoController : ApiController
    {
        private readonly AppContext _context = new AppContext();

        [Authorize]
        [HttpGet]
        public IHttpActionResult Get(string shortcut)
        {
            // Получаем ID фотографа с указанным shortcut
            var photographer = _context.Photographers.SingleOrDefault(x => x.Shortcut == shortcut);
            // Получаем все фото фотографа с указанным ID

            //var primaryQuery = _context.Photos.Where(x => x.PhotographerID == photographer.ID).As<IList<PhotoViewModel.Summary>>();
            return Ok(photographer.Photos.As<IList<PhotoViewModel.Summary>>());
        }

        public class FileUploadModel
        {
            public string FileName;
            public string Extension;
            public string Url;
            public string Width;
            public string Height;
            public string Shortcut;
        }

        [HttpPost]
        public IHttpActionResult Post(FileUploadModel model)
        {
            var userId = this.User.GetUserInformationID();
            var photographer = _context.Photographers.SingleOrDefault(x => x.Shortcut == model.Shortcut);

            _context.Photos.Add(new Photo
            {
                ID = Guid.NewGuid(),
                Description = "",
                Title = "",
                FileName = model.FileName,
                UploadDate = DateTime.Now,
                Url = model.Url
            });
                
            _context.SaveChanges();
            return Ok();
        }
    }
}