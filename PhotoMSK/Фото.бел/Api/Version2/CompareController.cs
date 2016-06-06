using System;
using System.Web.Http;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using Fotobel.Classes;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Attachments;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2
{
    [Authorize]
    public class CompareController : ApiController
    {
        private readonly AppContext _context = new AppContext();

        //GET api/compare/ - Generate a pair
        [HttpGet]
        public IHttpActionResult Get()
        {
            var date = DateTime.Now.Date;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);

            // Берем лист из 2х фото
            var primaryQuery = _context.NominationPhotos.Where(x => x.Month.DateMonth == firstDayOfMonth)
                        .OrderBy(x => x.CompareCount)
                        .ThenBy(x => x.Point).ToList();

            // Проверка наличия пары-юзера в Compare
            var userId = this.User.GetUserInformationID();

            var bc = new BestCompares(_context.Compares.Where(x => x.UserID == userId).ToList().Select(x => new ComparePair(x)));

            return Ok(bc.GetBestPair(primaryQuery).As<IList<NominantPhotoViewModel.Summary>>());
        }


        //POST /api/compare - Create a new compare.
        [HttpPost]
        public IHttpActionResult Post(List<string> photos)
        {
            var userId = this.User.GetUserInformationID();
            var comparePairModel = photos;

            var nomphoto1 = comparePairModel[0];
            var nomphoto2 = comparePairModel[1];

            var photo1 = _context.NominationPhotos.SingleOrDefault(x => x.PhotoID.ToString() == nomphoto1);
            var photo2 = _context.NominationPhotos.SingleOrDefault(x => x.PhotoID.ToString() == nomphoto2);

            _context.Compares.Add(new Compare
            {
                ID = Guid.NewGuid(),
                NominationPhoto1 = photo1,
                NominationPhoto2 = photo2,
                UserID = userId
            });

            _context.SaveChanges();
            return Ok();
        }

        //PUT /api/compare/{id} - Update ComparePoint.
        [HttpPut]
        public IHttpActionResult Put(Guid id, NominationPhoto photo)
        {
            var photoId = id;

            // Добавляем CompareCount участвовавшим   
            var countUp = _context.NominationPhotos.Single(x => x.PhotoID == photoId);
            countUp.CompareCount = countUp.CompareCount + 1;

            _context.SaveChanges();
            return Ok(true);
        }

        //DELETE /api/compare/{id} - Delete a compare pair.
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            var pair = _context.Compares.Find(id);
            _context.Entry(pair).State = EntityState.Deleted;
            _context.SaveChanges();
            return Ok();
        }
    }

    public class BestCompares
    {
        private IEnumerable<ComparePair> pairs;

        public BestCompares(IEnumerable<ComparePair> pairs)
        {
            this.pairs = pairs;
        }

        public IEnumerable<NominationPhoto> GetBestPair(IEnumerable<NominationPhoto> photos)
        {
            foreach (var nominationPhoto in photos)
            {
                var ordered = photos
                     .Where(nominationPhoto2 => nominationPhoto2.ID != nominationPhoto.ID)
                     .Where(nominationPhoto2 => !pairs.Any(x => x.IsCompared(nominationPhoto, nominationPhoto2)))
                     .OrderBy(x => pairs.Count(z => z.IsContains(x))).FirstOrDefault();

                if (ordered == null)
                    continue;
                yield return nominationPhoto;
                yield return ordered;
                yield break;
            }
            yield break;
        }
    }

    public class ComparePair
    {
        private readonly Guid[] _arr;

        public ComparePair(Compare compare)
        {
            _arr = new Guid[2] { compare.NominationPhoto1ID, compare.NominationPhoto2ID };
        }

        public bool IsCompared(NominationPhoto ph1, NominationPhoto ph2)
        {
            return _arr.Contains(ph1.ID) && _arr.Contains(ph2.ID);
        }
        public bool IsContains(NominationPhoto ph1)
        {
            return _arr.Contains(ph1.ID);
        }


        public Guid Right { get; set; }

        public Guid Left { get; set; }
    }
}
