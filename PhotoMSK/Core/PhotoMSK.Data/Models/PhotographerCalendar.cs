using System;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.Data.Models
{
    public class PhotographerCalendar : Calendar
    {
        public virtual Photographer Photographer { get; set; }

        public override RouteEntity RouteEntity
        {
            get { return Photographer; }
        }

        public override double GetPrice(DateTime start)
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            return "Съёмка";
        }

        public override string GetRouteName()
        {
            return "у" + Photographer;
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
}