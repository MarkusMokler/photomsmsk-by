using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Fotobel.Models;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.ActivityStream;
using PhotoMSK.Data.Models.Clients;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Activity;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2.Route
{
    public class ClientsController : ApiController
    {

        readonly AppContext _context = new AppContext();

        [HttpGet]
        public UserInformationViewModel.Client Get(string shortcut, Guid clientID)
        {
            var route = _context.Routes.Single(x => x.Shortcut == shortcut);

            Guid[] calendarsID;
            var photostudio = route as PhotoMSK.Data.Models.Routes.Photostudio;
            if (photostudio != null)
            {
                calendarsID = photostudio.Halls.Select(x => x.Calendar.ID).ToArray();
            }
            else
            {
                calendarsID = new Guid[] { };
            }

            var ui = _context.UserInformations.Find(clientID);
            var client = ui.As<UserInformationViewModel.Client>();
            client.Tags = ui.Categories.Select(z => z.Tag).ToList().As<IList<UserInformationViewModel.TagViewModel>>();

            client.Total = ui.Events.Where(x => x.Calendar != null && calendarsID.Contains(x.Calendar.ID)).Sum(x => x.Price);
            client.TotalDiscarded = ui.Events.Where(x => x.Calendar == null).Sum(x => x.Price);
            client.TotalPenaltied = ui.Penalties.Sum(x => x.Price);

            var events = _context.EventActivities.Where(x => x.Event.UserInformationID == clientID).ToList();
            client.Bookings = events.Count(x => x.State == EventActivityState.Created);
            client.Movings = events.Count(x => x.State == EventActivityState.Moved);
            client.Discards = events.Count(x => x.State == EventActivityState.Missed || x.State == EventActivityState.Removed);
            
            return client;
        }

        [HttpGet]
        public IList<UserInformationViewModel.TagViewModel> Get(string shortcut, bool ClientsCategoties)
        {
            var route = _context.Routes.Single(x => x.Shortcut == shortcut);

            return route.RouteClientCategories.Select(x => x.Tag).Distinct().ToList().As<IList<UserInformationViewModel.TagViewModel>>();
        }

        [HttpGet]
        public PageView<UserInformationViewModel.Client> Get(string shortcut, DateTime? from = null, int page = 0, int size = 20, string tag = null)
        {

            var route = _context.Routes.Single(x => x.Shortcut == shortcut);

            var photostudio = route as PhotoMSK.Data.Models.Routes.Photostudio;
            if (photostudio != null)
            {
                var calendars = photostudio.Halls.Select(x => x.Calendar.ID).ToList();

                var start = from ?? DateTime.Now.AddMonths(-3);

                var query =
                    _context.Events.Include(x => x.User)
                        .Include(x => x.User.Categories.Select(z => z.Tag))
                        .Where(x => x.Start > start && calendars.Contains(x.Calendar.ID));

                if (tag != null)
                    query = query.Where(x => x.User.Categories.Any(z => z.Tag.Name == tag));

                var groups = query.GroupBy(x => x.User);

                var client = groups.OrderByDescending(x => x.Sum(z => z.Price)).Skip(page * size).Take(size).ToList().Select(
                          x =>
                          {
                              var data = x.Key.As<UserInformationViewModel.Client>();
                              data.Tags =
                                  x.Key.Categories.Select(z => z.Tag)
                                      .ToList()
                                      .As<IList<UserInformationViewModel.TagViewModel>>();
                              data.Total = x.Sum(z => z.Price);
                              return data;
                          })
                      .ToList();
                return new PageView<UserInformationViewModel.Client>()
                {
                    Elements = client,
                    ItemsCount = groups.Count(),
                    PageSize = size
                };
            }
            throw new NotImplementedException("Not implemented for current route");
        }

        [HttpPost]
        public IHttpActionResult Post(string shortcut, ClientViewModel model)
        {

            var route = _context.Routes.Single(x => x.Shortcut == shortcut);

            var clientTags = route.RouteClientCategories.Where(x => x.UserID == model.ID);

            var routeClientCategories = clientTags as IList<RouteClientCategory> ?? clientTags.ToList();
            var removedTags = routeClientCategories.Where(x => model.Tags.Select(z => z.Text).ToList().Contains(x.Tag.Name) == false);

            foreach (var routeClientCategory in removedTags)
            {
                _context.Entry(routeClientCategory).State = EntityState.Deleted;
            }


            var existsTags = model.Tags.Where(x => routeClientCategories.Select(z => z.Tag.Name).ToList().Contains(x.Text) == false);

            foreach (var existsTag in existsTags)
            {

                route.RouteClientCategories.Add(new RouteClientCategory()
                {
                    ID = Guid.NewGuid(),
                    UserID = model.ID,
                    Tag = _context.Tags.SingleOrDefault(x => x.Name == existsTag.Text) ?? new Tag()
                    {
                        ID = Guid.NewGuid(),
                        Name = existsTag.Text
                    }
                });
            }

            _context.SaveChanges();
            return Ok();
        }

        public class ClientViewModel
        {
            public Guid ID { get; set; }
            public List<UserInformationViewModel.TagViewModel> Tags { get; set; }

        }

    }
}