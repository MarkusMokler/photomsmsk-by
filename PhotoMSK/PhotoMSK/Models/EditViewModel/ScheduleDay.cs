using System;

namespace PhotoMSK.Models.EditViewModel
{
    public class ScheduleDayEditModel : AbstractEditModel
    {
        public int? DayOfWeek { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public double? Price { get; set; }
        public Guid? HallID { get; set; }
    }
}