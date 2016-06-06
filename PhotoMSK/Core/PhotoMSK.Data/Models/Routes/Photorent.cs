using System;
using System.Collections.Generic;

namespace PhotoMSK.Data.Models.Routes
{
    public class Photorent : RouteEntity
    {
        public string Name { get; set; }
        public double CurrencyValue { get; set; }
        public int LastCaldendarCode { get; set; }

        public void AddCalendar(RentCalendar calendar)
        {
            calendar.BarCode = LastCaldendarCode + 1;
            LastCaldendarCode++;
            RentCalendars.Add(calendar);
        }


        public virtual ICollection<RentCalendar> RentCalendars { get; set; }
        public override string ToString()
        {
            return Name;
        }

        public override string GetName()
        {
            return Name;
        }
    }
}