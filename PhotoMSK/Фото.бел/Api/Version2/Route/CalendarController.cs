using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Fotobel.Classes;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Permission;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels;

namespace Fotobel.Api.Version2
{
    public class CalendarController : ApiController
    {
        private readonly AppContext _context = new AppContext();

        /// <summary>
        /// Return calendars date with friendly route
        /// </summary>
        /// <param name="routeid"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(string shortcut, DateTime start, DateTime end)
        {

            var route = _context.Routes.Single(x => x.Shortcut == shortcut);

            var cc = GetCalendars(route, start, end);
            var list = new List<RouteGroupViewModel> { cc };
            var routes = _context.Friends.Where(x => x.BaseRouteID == route.ID).Select(x => x.ChildRoute).ToList();

            list.AddRange(routes.Select(routeEntity => GetCalendars(routeEntity, start, end)));

            return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult GetEventDetails(Guid id)
        {
            var ev = _context.Events.Find(id);
            var name = ev.Calendar.GetName();
            return Ok(name);
        }


        private RouteGroupViewModel GetCalendars(RouteEntity route, DateTime start, DateTime end)
        {
            var date = start > DateTime.Now;

            var studio = route as PhotoMSK.Data.Models.Routes.Photostudio;
            if (studio != null)
            {
                var cals = studio.Halls.Select(x => x.Calendar).ToList();

                return new RouteGroupViewModel
                {
                    Name = studio.Name,
                    Calendars = cals.Select(x => new CalendarViewModel
                    {
                        CalendarID = x.ID,
                        Image = x.Hall.TeaserImageUrl,
                        Price = x.GetCost(start, end),
                        Title = x.GetName(),
                        CanBook = x.CanAdd(start, end) && date
                    }).ToList()
                };

            }
            var rent = route as Photorent;
            if (rent != null)
            {
                var cals = rent.RentCalendars.Take(10).ToList();

                return new RouteGroupViewModel()
                {
                    Name = rent.Name,
                    Calendars = cals.Select(x => new CalendarViewModel
                    {
                        CalendarID = x.ID,
                        Image = x.Phototechnics.TeaserImage,
                        Price = x.GetCost(start, end),
                        Title = x.GetName(),
                        CanBook = x.CanAdd(start, end) && date
                    }).ToList()
                };
            }
            throw new NotImplementedException();
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }

    internal class RouteGroupViewModel
    {
        public string Name { get; set; }
        public List<CalendarViewModel> Calendars { get; set; }
    }
}
