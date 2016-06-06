using System;
using System.Collections.Generic;

namespace PhotoMSK.Data.Models
{
    public abstract class Calendar : UniqueEntity
    {
        protected Calendar()
        {
            ID = Guid.NewGuid();
        }

        public virtual ICollection<Event> Events { get; set; }
        public string Color { get; set; }
        public abstract RouteEntity RouteEntity { get; }
        public abstract double GetPrice(DateTime start);
        public abstract string GetName();
        public abstract string GetRouteName();

        public abstract bool CanAdd(DateTime start, DateTime end);

        public abstract double GetCost(DateTime start, DateTime end);

    }
}