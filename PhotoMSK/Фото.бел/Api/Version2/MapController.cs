using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fotobel.Api.Version2.Route;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;

namespace Fotobel.Api.Version2
{
    public class MapController : ApiController
    {
        private readonly AppContext _context = new AppContext();

        [HttpGet]
        public IList<PhotostudioViewModel.Summary> Get()
        {
            var result = _context.Photostudios.OrderBy(x => x.Longitude).As<IList<PhotostudioViewModel.Summary>>();
            return result;
        }
    }
}
