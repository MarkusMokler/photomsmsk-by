using System;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.InteropServices;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.Data.Models
{
    public class RentCalendar : Calendar
    {
        public virtual Guid PhotorentID { get; set; }
        public virtual Guid PhototechnicsID { get; set; }
        public double HourlyPrice { get; set; }
        public double HalfDayPrice { get; set; }
        public double DaylyPrice { get; set; }
        public double HollidayPrice { get; set; }
        public double WeeklyPrice { get; set; }
        public double MonthlyPrice { get; set; }
        public int BarCode { get; set; }


        public virtual Photorent Photorent { get; set; }

        public virtual Phototechnics Phototechnics { get; set; }

        public override RouteEntity RouteEntity
        {
            get { return Photorent; }
        }

        public override double GetPrice(DateTime start)
        {
            bool hollyday = start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday;
            return hollyday ? HollidayPrice : DaylyPrice;
        }

        public override string GetName()
        {
            return Phototechnics.Name;
        }

        public override string GetRouteName()
        {
            return Photorent.Name;
        }

        public override bool CanAdd(DateTime start, DateTime end)
        {
            if (!RouteEntity.AllowOnlineBooking)
                return false;

            return !Events.Where(x => x.EventState != EventState.Deleted).Any(x => (x.Start < start && start < x.End) || (x.Start < end && end < x.End));
        }

        public override double GetCost(DateTime start, DateTime end)
        {
            var dof = Convert.ToInt32(start.DayOfWeek);

            if (dof == 0)
                dof = 7;

            var days = (end - start).TotalDays;

            if (days > 3)
                Math.Min(Math.Min(Math.Ceiling(days / 7) * WeeklyPrice, Math.Ceiling(days / 30) * MonthlyPrice), days * DaylyPrice);

            var hours = (end - start).TotalHours;

            return Math.Min(Math.Min(Math.Ceiling(hours / 3) * HalfDayPrice * Photorent.CurrencyValue, dof < 5 ? DaylyPrice : HollidayPrice * Photorent.CurrencyValue), hours * HourlyPrice * Photorent.CurrencyValue);

        }
    }

    public class RentCalendarConfiguration : EntityTypeConfiguration<RentCalendar>
    {
        public RentCalendarConfiguration()
        {
            ToTable("RentCalendars");
            HasRequired(x => x.Photorent).WithMany(x => x.RentCalendars).HasForeignKey(x => x.PhotorentID);
            HasRequired(x => x.Phototechnics).WithMany(x => x.Rents).HasForeignKey(x => x.PhototechnicsID);
        }
    }
}