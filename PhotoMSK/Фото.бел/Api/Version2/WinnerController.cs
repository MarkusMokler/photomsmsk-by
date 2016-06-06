using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Attachments;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2
{
    public class WinnerController : ApiController
    {
        private readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get()
        {
            // Get the last month
            var date = DateTime.Now.Date;
            var firstDayOfMonth = new DateTime(date.Year, date.Month - 1, 1);
            var lastMonth = _context.Months.Single(x => x.DateMonth == firstDayOfMonth);

            // Generate places
            Random rnd = new Random();
            int randomPercent = rnd.Next(1, 101);

            var place = 0;
            var placeLabel = "";
            string[] labels = { "Победитель", "2 место", "3 место", "4 место", "5 место" };

            // Winner - 45%, 2 - 25%, 3 - 15%, 4 - 10%, 5 - 5%
            if (randomPercent >= 0 && randomPercent < 45)
            {
                place = 0;
                placeLabel = labels[0];
            }
            else if (randomPercent >= 45 && randomPercent < 70)
            {
                place = 1;
                placeLabel = labels[1];
            }
            else if (randomPercent >= 70 && randomPercent <= 85)
            {
                place = 2;
                placeLabel = labels[2];
            }
            else if (randomPercent >= 85 && randomPercent <= 95)
            {
                place = 3;
                placeLabel = labels[3];
            }
            else if (randomPercent >= 95 && randomPercent <= 100)
            {
                place = 4;
                placeLabel = labels[4];
            }

            // Get the winner
            var query = _context.NominationPhotos.Where(x => x.MonthID == lastMonth.ID)
                                 .OrderByDescending(x => x.Point)
                                 .Take(5);
            var winner = query.ToList()[place];
            var result = winner.As<NominantPhotoViewModel.Summary>();
            result.Place = placeLabel;

            return Ok(result);
        }

        [Authorize]
        //PUT /api/compare/{id} - Update winner.
        [HttpPut]
        public IHttpActionResult Put(Guid id, NominationPhoto photo)
        {
            var photoId = id;

            // Ищем победителя по ID и добавляем голос
            var updateNominations = _context.NominationPhotos.Where(x => x.PhotoID == photoId);
            var winnerPhoto = updateNominations.Single();
            winnerPhoto.Point = winnerPhoto.Point + 1;

            _context.SaveChanges();
            return Ok(true);
        }
    }
}
