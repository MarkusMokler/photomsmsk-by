using System;

namespace PhotoMSK.Data.Models
{
    public class ScheduleDay
    {
        public Guid ID { get; set; }
        public int DayOfWeek { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public double Price { get; set; }
        public Guid HallID { get; set; }
        public virtual Hall Hall { get; set; }
    }
}