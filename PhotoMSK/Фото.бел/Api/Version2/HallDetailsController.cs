using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Event;
using PhotoMSK.ViewModels.Realisation;
using Vk.SDK.Model;

namespace Fotobel.Api.Version2
{
    public class HallController : ApiController
    {
        private readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(int from = 0, int pageSize = 9)
        {
            var query = _context.Halls.Where(x => x.Photostudio.Active);

            var elements = query.
                OrderBy(x => x.ID)
                .Skip(from)
                .Take(pageSize).ToList();

            return Ok(new PageView<HallViewModel.Summary>
            {
                Elements = elements.As<IList<HallViewModel.Summary>>(),
                PageSize = pageSize,
                ItemsCount = query.Count()
            });
        }


        [HttpGet]
        public IHttpActionResult GetDetails(Guid id)
        {
            var result = new PageView<HallViewModel.Details>()
            {
                Elements = _context.Halls.Where(a => a.ID == id).OrderBy(x => x.Name).As<IList<HallViewModel.Details>>(),
                ItemsCount = 1,
                PageSize = 1

            };
            return Ok(result);
        }
    }
}
