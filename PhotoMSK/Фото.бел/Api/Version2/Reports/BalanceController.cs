using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Statistics;
using Fotobel.Classes;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Statistic;

namespace Fotobel.Api.Version2.Reports
{
    public class BalanceController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(string shortcut)
        {
            IList<Event> events = null;

            var route = _context.Routes.Single(x => x.Shortcut == shortcut);
            DateTime dd = DateTime.Today.AddDays(1);

            if (route is PhotoMSK.Data.Models.Routes.Photostudio)
                events =
                    (route as PhotoMSK.Data.Models.Routes.Photostudio).Halls.Select(x => x.Calendar)
                        .SelectMany(x => x.Events)
                        .Where(x => x.Start > dd)
                        .ToList();

            if (route is PhotoMSK.Data.Models.Routes.Photorent)
                events =
                    (route as PhotoMSK.Data.Models.Routes.Photorent).RentCalendars.SelectMany(x => x.Events)
                        .Where(x => x.Start > dd)
                        .ToList();

            Dictionary<int, BalanceLineViewModel.Summary> devents = new Dictionary<int, BalanceLineViewModel.Summary>();

            foreach (var @event in events)
            {
                if (devents.ContainsKey(@event.Start.DayOfYear) == false)
                    devents.Add(@event.Start.DayOfYear, new BalanceLineViewModel.Summary
                    {
                        BalanceLineType = BalanceLineType.Planed,
                        Date = @event.Start.Date,
                        Title = "Ожидаемые поступления за " + @event.Start.Date.ToShortDateString(),
                        WeekOfMonth = @event.Start.GetWeekOfMonth(),
                        WeekOfYear = @event.Start.GetWeekOfYear(),
                        Year = @event.Start.Year

                    });
                devents[@event.Start.DayOfYear].AddEvent(@event);
            }

            var datetime = DateTime.Today.AddDays(-14);
            var woy = datetime.GetWeekOfYear();
            var y = datetime.Year;

            var dbweeks = _context.WeekBalances
                .Where(x => x.RouteID == route.ID && x.WeekOfYear >= woy && x.Year >= y)
                .OrderBy(x => x.Year)
                .ThenBy(x => x.WeekOfYear).ToList().As<IList<WeekBalanceViewModel.Details>>();




            Dictionary<int, WeekBalanceViewModel.Details> weeks = dbweeks.ToDictionary(weekBalance => weekBalance.WeekOfYear);

            foreach (var devent in devents)
            {

                if (weeks.ContainsKey(devent.Value.WeekOfYear.Value) == false)
                    weeks.Add(devent.Value.WeekOfYear.Value, new WeekBalanceViewModel.Details
                    {
                        WeekOfMonth = devent.Value.WeekOfMonth.Value,
                        WeekOfYear = devent.Value.WeekOfYear.Value,
                    });
                weeks[devent.Value.WeekOfYear.Value].AddBalance(devent.Value);
            }

            var objects = weeks.Values.OrderBy(x => x.WeekOfYear).ToList();

            var dbalance = objects.First().StartBalance;

            foreach (var vari in objects)
            {
                vari.StartBalance = dbalance;
                dbalance += vari.BalanceLines.Sum(balanceLine => balanceLine.Total);
                vari.BalanceLines = vari.BalanceLines.OrderBy(x => x.Date).ToList();
                vari.EndBalance = dbalance;
            }

            return Ok(objects);
        }


        [HttpPost]
        [Message("День закрыт упешно")]
        public IHttpActionResult Post(string shortcut, BalanceData balance)
        {
            var route = _context.Routes.Single(x => x.Shortcut.Equals(shortcut));

            DateTime bdate = balance.GetDate();
            var week = bdate.GetWeekOfYear();

            var weekBalance = _context.WeekBalances.SingleOrDefault(x => x.RouteID == route.ID && x.WeekOfYear == week && x.Year == bdate.Year) ??
                              new WeekBalance()
                              {
                                  ID = Guid.NewGuid(),
                                  Closed = false,
                                  RouteID = route.ID,
                                  WeekOfYear = bdate.GetWeekOfYear(),
                                  WeekOfMonth = bdate.GetWeekOfMonth(),
                                  Year = bdate.Year
                              };


            var title = balance.Type == BalanceLineType.Getted ? "Выручка за " + bdate.ToShortDateString() : balance.Title;

            var balanceLine = new BalanceLine
            {
                ID = Guid.NewGuid(),
                Route = route,
                RouteID = route.ID,
                WeekBalance = weekBalance,
                Date = bdate,
                WeekOfYear = bdate.GetWeekOfYear(),
                WeekOfMonth = bdate.GetWeekOfMonth(),
                Year = bdate.Year,
                BalanceLineType = balance.Type,
                Total = balance.Total,
                Title = title
            };

            _context.BalanceLines.Add(balanceLine);
            _context.SaveChanges();
            return Ok();
        }


        public class BalanceData
        {
            public BalanceLineType Type { get; set; }
            public double Total { get; set; }
            public string Title { get; set; }
            public DateTime? Date { get; set; }
            public DateTime GetDate()
            {
                return Date ?? DateTime.Today;
            }
        }
    }
}