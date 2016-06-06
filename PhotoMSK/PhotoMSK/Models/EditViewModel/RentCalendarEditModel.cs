using System;

namespace PhotoMSK.Models.EditViewModel
{
    public class RentCalendarEditModel : AbstractEditModel
    {
        public virtual Guid? PhotorentID { get; set; }
        public virtual Guid? PhototechnicsID { get; set; }
        public double? HourlyPrice { get; set; }
        public double? HalfDayPrice { get; set; }
        public double? DaylyPrice { get; set; }
        public double? HollidayPrice { get; set; }
        public double? WeeklyPrice { get; set; }
        public double? MonthlyPrice { get; set; }

    }
}