using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Attachments;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Routes;
using PHS = PhotoMSK.Data.Models.Routes.Photostudio;

namespace Fotobel.Api.Version2
{
    [Authorize]
    public class HomeController : ApiController
    {
        private readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get()
        {

            var t = _context.Users.First(x => x.UserName == User.Identity.Name)
            .UserInformation.Roles.Where(x => x.AccessStatus == AccessStatus.Owner || x.AccessStatus == AccessStatus.Administrator)
            .Select(x => x.ID).ToList();


            var y = _context.Participants.Where(x => t.Contains(x.ID)).Include(x=>x.Route).Select(x=>x.Route.ID).ToList();

            var data = new List<Object>();
            
            var routes = _context.Routes.Where(x => y.Contains(x.ID))
                .Include(x => x.Participants)
                .Include(x => x.Raiting)
                .Include(x => x.Phones)
                .Include(x => x.Participants.Select(z => z.UserInformation));
            data.AddRange(routes.OfType<PHS>().Include(x => x.Halls).Include(x => x.Halls.Select(z=>z.Schedule)).Include(x => x.Halls.Select(z => z.Calendar)).ToList().As<IList<PhotostudioViewModel.Summary>>());

            data.AddRange(routes.Where(x => x is PhotoMSK.Data.Models.Routes.Photographer).As<IList<PhotographerViewModel.Summary>>());

            data.AddRange(routes.Where(x => x is PhotoMSK.Data.Models.Routes.Photomodel).As<IList<PhotomodelViewModel.Summary>>());

            data.AddRange(routes.Where(x => x is PhotoMSK.Data.Models.Routes.Page).As<IList<PageViewModel.Summary>>());

            data.AddRange(routes.Where(x => x is PhotoMSK.Data.Models.Routes.Masterclass).As<IList<MasterclassViewModel.Summary>>());

            data.AddRange(routes.OfType<Photorent>().ToList().As<IList<PhotorentViewModel.Summary>>());

            data.AddRange(routes.OfType<PhotoMSK.Data.Models.Routes.Photoshop>().ToList().As<IList<PhotoshopViewModel.Summary>>());
            return Ok(data);
        }
    }
}
