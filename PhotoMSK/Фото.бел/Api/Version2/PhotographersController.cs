using PhotoMSK.Data;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Fotobel.Api.Version2
{
    public class PhotographersController : ApiController
    {
        private readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get()
        {
            var photographers = _context.Photographers.ToList();
            var thelist = new List<Photographer>();

            foreach (Photographer photographer in photographers.Where(photographer => photographer.Country == null))
            {
                if (photographer.Phones.Single().Phone.ToString().Substring(0, 3) == "+375")
                {
                    photographer.Country = "Беларусь";
                }
                else if (photographer.Phones.Single().Phone.ToString().Substring(0, 1) == "+7")
                {
                    photographer.Country = "Россия";
                }
                else if (photographer.Phones.Single().Phone.ToString().Substring(0, 3) == "+380")
                {
                    photographer.Country = "Украина";
                }
                thelist.Add(photographer);
            }

            var res = new PageView<PhotographerViewModel.Summary>()
            {
                Elements = thelist.As<IList<PhotographerViewModel.Summary>>()
            };
            return Ok(res);
        }

        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            var userId = new Guid(id);
            var roles = _context.Participants;
            var photographers = _context.Photographers.Select(x => x.ID).ToList();

            var result = roles.Where(p => p.UserInformation.ID == userId)
                              .Where(p => photographers.Contains(p.Route.ID))
                              .Select(p => p.Route.ID);

            var userPhotographers = new List<Photographer>();

            userPhotographers = _context.Photographers.Where(x => result.Contains(x.ID))
                                      .OrderBy(x => x.Pro)
                                      .ThenBy(x => x.Verified)
                                      .ThenByDescending(x => x.Raiting.Total)
                                      .ToList();

            return Ok(userPhotographers.As<IList<PhotographerViewModel.Summary>>());
        }
    }
}