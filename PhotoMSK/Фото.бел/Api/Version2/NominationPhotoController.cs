using System;
using System.Collections.Generic;
using System.Web.Http;
using Fotobel.Classes;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Attachments;
using System.Linq;

namespace Fotobel.Api.Version2
{
    [Authorize]
    public class NominationPhotoController : ApiController
    {
        private readonly AppContext _context = new AppContext();

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

            // Получаем первый день месяца и берем ID этого месяца из Months
            var date = DateTime.Now.Date;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var mydate = _context.Months.Single(x => x.DateMonth == firstDayOfMonth);
            var photographer = _context.Photographers.SingleOrDefault(x => x.Shortcut == model.Shortcut);

            _context.NominationPhotos.Add(new NominationPhoto
            {
                ID = Guid.NewGuid(),
                CompareCount = 0,
                Point = 0,
                Month = mydate,
                Photo = new Photo
                {
                    ID = Guid.NewGuid(),
                    Description = "",
                    Title = "",
                    FileName = model.FileName,
                    UploadDate = DateTime.Now,
                    Url = model.Url
                },
                Photographer = photographer
            });

            _context.SaveChanges();
            return Ok();
        }
    }
}