using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Menu;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2
{
    public class HallSearchController : ApiController
    {
        AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(string name)
        {
            var hall = _context.Halls.Where(x => x.Name.Contains(name)).ToList();
            return Ok(hall.As<IList<HallViewModel.Details>>());
        }
    }
}
