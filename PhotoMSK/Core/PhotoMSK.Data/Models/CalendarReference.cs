using System;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models
{
    public class CalendarReference
    {
        public Guid ID { get; set; }
        public string CopyFrom { get; set; }
        public DateTime LastCollectTime { get; set; }
        public virtual Guid CalendarID { get; set; }
        public virtual Calendar Calendar { get; set; }
    }

    public class CalendarReferenceConfigurator : EntityTypeConfiguration<CalendarReference>
    {
        public CalendarReferenceConfigurator()
        {
            HasKey(x => x.ID);
            HasRequired(x => x.Calendar).WithMany().HasForeignKey(x => x.CalendarID);
        }
    }
}