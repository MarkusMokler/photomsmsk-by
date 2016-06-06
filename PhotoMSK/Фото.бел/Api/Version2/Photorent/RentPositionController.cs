using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;
using Fotobel.Models;

namespace PhotoMSK.Api.v1.PriceControllers
{
    [Authorize]
    public class RentPositionController : ApiController
    {
        readonly AppContext _contex = new AppContext();


        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get([FromUri]Guid routeid, [FromUri]Guid categoryid, [FromUri]Guid? brandid = null, [FromUri]string order = "", [FromUri]int page = 0, [FromUri]int size = 20)
        {
            var query = _contex.RentCalendars.Include(x => x.Photorent)
                .Where(x => x.PhotorentID == routeid && x.Phototechnics.Category.ID == categoryid);
            if (brandid.HasValue)
                query = query.Where(x => x.Phototechnics.Brand.ID == brandid.Value);

            switch (order)
            {
                case "price":
                    {
                        query = query.OrderBy(x => x.DaylyPrice);
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

            var pg = new PageView<ViewModels.Route.PhototechnicsViewModel.Summary>
            {
                ItemsCount = query.Count(),
                PageSize = size,
                Elements =
                    query.Skip(page * size).Take(size).ToList().As<IList<ViewModels.Route.PhototechnicsViewModel.Summary>>()
            };
            return Ok(pg);
        }

        [HttpPut]
        public IHttpActionResult Put(Guid id, Proxy<RentCalendar>  model)
        {
            var cal = _contex.RentCalendars.Find(model.Model.ID);
            model.Patch(cal);
            _contex.SaveChanges();
            return Ok(cal);
        }
    }

}
