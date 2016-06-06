using System;
using System.Collections.Generic;
using System.Linq;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Data.Models.Widgets;

namespace PhotoMSK.Data.Models
{
    public class Hall : SortedEntry
    {
        public string Name { get; set; }
        public Guid PhotostudioID { get; set; }
        public decimal Square { get; set; }
        public string Description { get; set; }
        public HallTypes HallType { get; set; }
        public virtual HallCalendar Calendar { get; set; }
        public virtual ICollection<ScheduleDay> Schedule { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<HallProperty> HallProperties { get; set; }
        public virtual Photostudio Photostudio { get; set; }

        public Guid? ZoneID { get; set; }
        public virtual Zone Zone { get; set; }
        public string TeaserImage { get; set; }

        public string TeaserImageUrl
        {
            get
            {
                if (TeaserImage != null)
                {
                    return TeaserImage;
                }
                Photo b = null;
                if (Photos != null)
                    b = Photos.OrderByDescending(x => x.UploadDate).FirstOrDefault();
                return b != null ? b.Url : "/Content/images/no-avatar.png";
            }
        }

        public double GetDaylyPrice(DateTime start)
        {
            int dof = Convert.ToInt32(start.DayOfWeek);
            if (dof == 0)
                dof = 7;
            return Schedule.Single(x => x.DayOfWeek == dof).Price;
        }

        public bool CanAdd(DateTime start, DateTime end)
        {
            int dof = Convert.ToInt32(start.DayOfWeek);
            if (dof == 0)
                dof = 7;
            var schedule = Schedule.Single(x => x.DayOfWeek == dof);
            if (schedule.TimeStart.Hour > start.Hour)
                return false;
            if (schedule.TimeEnd.Hour < end.Hour)
                return false;
            return true;
        }

    }

}