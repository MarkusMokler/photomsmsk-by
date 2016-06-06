using System;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models.Routes
{
    public class DressCalendar : Calendar
    {
        public virtual Dress Dress { get; set; }
        public override RouteEntity RouteEntity => Dress.DressRent;

        public override double GetPrice(DateTime start)
        {
            return Dress.GetPrice(start);
        }

        public override string GetName()
        {
            return Dress.Name;
        }

        public override string GetRouteName()
        {
            return RouteEntity.GetName();
        }

        public override bool CanAdd(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public override double GetCost(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
    public class DressCalendarConfiguration : EntityTypeConfiguration<DressCalendar>
    {
        public DressCalendarConfiguration()
        {
            ToTable("DressCalendar");
                
        }
    }
}