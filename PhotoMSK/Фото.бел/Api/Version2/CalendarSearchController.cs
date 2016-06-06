using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2
{
    public class CalendarSearchController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(string name)
        {
            var list = _context.Halls.Where(x => x.Name.Contains(name)).ToList();
            var allList = new List<CalendarViewModel>();
            var hallcalendars = list.Select(x => new CalendarViewModel
            {
                ID = x.ID,
                CanBook = x.Photostudio.AllowOnlineBooking,
                Title = x.Name,
                Image = x.TeaserImageUrl,
                CalendarID = x.Calendar.ID,
                Price = x.GetDaylyPrice(DateTime.Now),
                RouteName = x.Photostudio.Name
            }).ToList();
            var rentList = _context.RentCalendars.Where(x => x.Phototechnics.Name.Contains(name)).ToList();
            var rentCalendars = rentList.Select(x => new CalendarViewModel
            {
                ID = x.ID,
                CanBook = x.Photorent.AllowOnlineBooking,
                Title = x.Phototechnics.Name,
                Image = x.Phototechnics.TeaserImage,
                Color = x.Color,
                CalendarID = x.ID,
                RouteName = x.Photorent.Name
            }).ToList();
            allList.AddRange(hallcalendars);
            allList.AddRange(rentCalendars);
            return Ok(allList);
        }

        [HttpGet]
        public IHttpActionResult GetDetails([FromUri]List<Guid> ids)
        {

            var list = _context.Calendars.Where(x => ids.Contains(x.ID)).ToList();
            var allList = new List<CalendarViewModel>();
            var hallcalendars = list.OfType<HallCalendar>().Select(x => new CalendarViewModel
            {
                ID = x.ID,
                CanBook = x.Hall.Photostudio.AllowOnlineBooking,
                Title = x.Hall.Name,
                Image = x.Hall.TeaserImageUrl,
                Color = x.Color,
                CalendarID = x.ID
            }).ToList();

            var rentCalendars = list.OfType<RentCalendar>().Select(x => new CalendarViewModel
            {
                ID = x.ID,
                CanBook = x.Photorent.AllowOnlineBooking,
                Title = x.Phototechnics.Name,
                Image = x.Phototechnics.TeaserImage,
                Color = x.Color,
                CalendarID = x.ID
            }).ToList();

            allList.AddRange(rentCalendars);
            allList.AddRange(hallcalendars);
            return Ok(allList);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri]List<Guid> ids, DateTime start, DateTime end)
        {

            var list = _context.Calendars.Where(x => ids.Contains(x.ID)).ToList();
            var allList = new List<CalendarViewModel>();
            var hallcalendars = list.OfType<HallCalendar>().Select(x => new CalendarViewModel
            {
                ID = x.ID,
                CanBook = x.CanAdd(start, end),
                Title = x.Hall.Name,
                Image = x.Hall.TeaserImageUrl,
                Color = x.Color,
                CalendarID = x.ID,
                Price = x.GetCost(start, end)
            }).ToList();

            var rentCalendars = list.OfType<RentCalendar>().Select(x => new CalendarViewModel
            {
                ID = x.ID,
                CanBook = x.CanAdd(start, end),
                Title = x.Phototechnics.Name,
                Image = x.Phototechnics.TeaserImage,
                Color = x.Color,
                CalendarID = x.ID,
                Price = x.GetCost(start, end),

            }).ToList();

            allList.AddRange(rentCalendars);
            allList.AddRange(hallcalendars);
            return Ok(allList);
        }

    }

}