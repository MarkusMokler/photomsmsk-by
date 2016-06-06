using System;
using System.Linq;

namespace PhotoMSK.Data.Models
{
    public class HallCalendar : Calendar
    {
        public virtual Hall Hall { get; set; }

        public override RouteEntity RouteEntity => Hall.Photostudio;

        public override double GetPrice(DateTime start)
        {
            return Hall.GetDaylyPrice(start);
        }

        public override string GetName()
        {
            return "зал " + Hall.Name;
        }

        public override string GetRouteName()
        {
            return "студия " + Hall.Photostudio.Name;
        }

        public override bool CanAdd(DateTime start, DateTime end)
        {
            if (!Hall.Photostudio.AllowOnlineBooking)
                return false;

            if (!Hall.CanAdd(start, end))
                return false;

            return !Events.Where(x=>x.EventState!=EventState.Deleted)
                .Any(x => (x.Start <= start && start < x.End) || (x.Start < end && end <= x.End) || (x.Start >= start && x.End <= end));

        }

        public override double GetCost(DateTime start, DateTime end)
        {
            return Hall.GetDaylyPrice(start) * (end - start).TotalHours;
        }
    }
}