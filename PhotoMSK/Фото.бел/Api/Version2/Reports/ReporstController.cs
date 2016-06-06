using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;

namespace Fotobel.Api.Version2.Reports
{
    public class CalendarMonthController : ApiController
    {
        private readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult HallMonthReport(string shortcut, DateTime date)
        {
            var di = DateTimeFormatInfo.CurrentInfo;

            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = new DateTime(date.Year, date.Month, di.Calendar.GetDaysInMonth(date.Year, date.Month)).AddDays(1);

            IList<Event> events = null;

            var route = _context.Routes.Single(x => x.Shortcut == shortcut);

            if (route is PhotoMSK.Data.Models.Routes.Photostudio)
                events = (route as PhotoMSK.Data.Models.Routes.Photostudio).Halls.Select(x => x.Calendar).SelectMany(x => x.Events).Where(x => x.Start > startDate && x.Start < endDate).ToList();

            if (route is PhotoMSK.Data.Models.Routes.Photorent)
                events = (route as PhotoMSK.Data.Models.Routes.Photorent).RentCalendars.SelectMany(x => x.Events).Where(x => x.Start > startDate && x.Start < endDate).ToList();


            var calendarLines = new Dictionary<string, CalendarLine>();
            CalendarLine total = new CalendarLine("Total", endDate.AddDays(-1));
            foreach (var ee in events)
            {
                if (!calendarLines.ContainsKey(ee.Calendar.GetName()))
                    calendarLines.Add(ee.Calendar.GetName(), new CalendarLine(ee.Calendar.GetName(), endDate.AddDays(-1)));

                calendarLines[ee.Calendar.GetName()].addEvent(ee);
                total.addEvent(ee);
            }

            return Ok(new MonthStatistic()
            {
                CalendarLines = calendarLines.Values.ToList(),
                Total = total,
                WeeksInMonth = endDate.AddDays(-1).GetWeekOfMonth()
            });
        }

        class MonthStatistic
        {
            public List<CalendarLine> CalendarLines { get; set; }
            public int WeeksInMonth { get; set; }
            public CalendarLine Total { get; set; }
        }

        class CalendarLine
        {
            readonly Dictionary<int, WeekObject> _weeks = new Dictionary<int, WeekObject>();
            public string Name { get; set; }

            public List<WeekObject> Weeks => _weeks.Values.ToList();
            public double TotalWeeks => _weeks.Select(x => x.Value).Sum(x => x.Week);
            public double TotalWeeksEnds => _weeks.Select(x => x.Value).Sum(x => x.WeekEnd);

            public double TotalWeeksHours => _weeks.Select(x => x.Value).Sum(x => x.WeekHours);
            public double TotalWeeksEndsHours => _weeks.Select(x => x.Value).Sum(x => x.WeekEndHours);
            public CalendarLine(string name, DateTime ee)
            {
                Name = name;
                for (int i = 1; i <= ee.GetWeekOfMonth(); i++)
                {
                    if (!_weeks.ContainsKey(i))
                        _weeks.Add(i, new WeekObject());

                }
            }


            public void addEvent(Event ev)
            {
                var week = ev.Start.GetWeekOfMonth();

                if (!_weeks.ContainsKey(week))
                    _weeks.Add(week, new WeekObject());
                _weeks[week].AddEvent(ev);
            }

        }

        class WeekObject
        {
            public double Week;
            public double WeekEnd;
            public double WeekHours;
            public double WeekEndHours;

            public void AddEvent(Event ev)
            {
                var di = DateTimeFormatInfo.CurrentInfo;
                var dd = di.Calendar.GetDayOfWeek(ev.Start);
                if (dd == DayOfWeek.Monday || dd == DayOfWeek.Thursday || dd == DayOfWeek.Wednesday ||
                    dd == DayOfWeek.Thursday)
                {
                    Week += ev.Price;
                    WeekHours += (ev.End - ev.Start).TotalHours;
                }
                else
                {
                    WeekEnd += ev.Price;
                    WeekEndHours += (ev.End - ev.Start).TotalHours;
                }

            }
        }
    }

    static class DateTimeExtensions
    {
        static readonly GregorianCalendar _gc = new GregorianCalendar();
        public static int GetWeekOfMonth(this DateTime time)
        {
            DateTime first = new DateTime(time.Year, time.Month, 1);
            return time.GetWeekOfYear() - first.GetWeekOfYear() + 1;
        }

        public static int GetWeekOfYear(this DateTime time)
        {
            return _gc.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }
    }
}
