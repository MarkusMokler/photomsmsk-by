using System;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models.ActivityStream
{
    public class EventActivity : Activity
    {
        public virtual Event Event { get; set; }
        public Guid EventID { get; set; }
        public EventActivityState State { get; set; }
    }
    public class RentCalendarConfiguration : EntityTypeConfiguration<EventActivity>
    {
        public RentCalendarConfiguration()
        {
            ToTable("EventActivity");
        }
    }
}