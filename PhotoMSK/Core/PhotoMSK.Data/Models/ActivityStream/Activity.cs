using System;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models.ActivityStream
{
    public abstract class Activity
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ActivityTime { get; set; }
        public Guid RouteID { get; set; }
        public virtual RouteEntity Route { get; set; }
        public Guid UserID { get; set; }
        public virtual UserInformation User { get; set; }

    }
    public class ActivityConfiguration : EntityTypeConfiguration<Activity>
    {
        public ActivityConfiguration()
        {
            ToTable("Activity");
        }
    }
}
