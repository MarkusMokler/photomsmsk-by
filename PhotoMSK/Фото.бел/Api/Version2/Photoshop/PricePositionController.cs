using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;

namespace Fotobel.Api.Version2.Photoshop
{
    public class PricePositionController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(string shortcut, Guid? brandid = null, [FromUri]string order = "", [FromUri]int page = 0, [FromUri]int size = 20)
        {
            var query = _context.PricePositions.Include(x => x.Phototechnics)
                .Where(x => x.Photoshop.Shortcut == shortcut);
            if (brandid.HasValue)
                query = query.Where(x => x.Phototechnics.Brand.ID == brandid.Value);

            switch (order)
            {
                case "price":
                    {
                        query = query.OrderBy(x => x.Price);
                        break;
                    }
                case "date":
                    {
                        //   query = query.OrderBy(x => x.PhototechnicsViewModel.);
                        break;
                    }
                default:
                    {
                        query = query.OrderBy(x => x.Phototechnics.Raiting.Total);
                        break;
                    }
            }

            var pg = new PageView<PhototechnicsViewModel.Summary>
            {
                ItemsCount = query.Count(),
                PageSize = size,
                Elements =
                    query.Skip(page * size).Take(size).ToList().As<IList<PhototechnicsViewModel.Summary>>()
            };
            return Ok(pg);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri]Guid routeid, [FromUri]Guid categoryid, [FromUri]Guid? brandid = null, [FromUri]string order = "", [FromUri]int page = 0, [FromUri]int size = 20)
        {
            var query = _context.PricePositions.Include(x => x.Phototechnics)
                .Where(x => x.PhotoshopID == routeid && x.Phototechnics.Category.ID == categoryid);
            if (brandid.HasValue)
                query = query.Where(x => x.Phototechnics.Brand.ID == brandid.Value);

            switch (order)
            {
                case "price":
                    {
                        query = query.OrderBy(x => x.Price);
                        break;
                    }
                case "date":
                    {
                        //   query = query.OrderBy(x => x.PhototechnicsViewModel.);
                        break;
                    }
                default:
                    {
                        query = query.OrderBy(x => x.Phototechnics.Raiting.Total);
                        break;
                    }
            }

            var pg = new PageView<PhototechnicsViewModel.Summary>
            {
                ItemsCount = query.Count(),
                PageSize = size,
                Elements =
                    query.Skip(page * size).Take(size).ToList().As<IList<PhototechnicsViewModel.Summary>>()
            };
            return Ok(pg);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
