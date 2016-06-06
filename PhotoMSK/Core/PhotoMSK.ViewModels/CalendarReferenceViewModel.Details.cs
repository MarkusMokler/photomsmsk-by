using System;

namespace PhotoMSK.ViewModels
{
    public static partial class CalendarReferenceViewModel
    {
        public class Details
        {
            public Guid ID { get; set; }
            public string CopyFrom { get; set; }
            public DateTime LastCollectTime { get; set; }
            public virtual Guid CalendarID { get; set; }
            public virtual CalendarViewModel Calendar { get; set; }
        }
    }
}
