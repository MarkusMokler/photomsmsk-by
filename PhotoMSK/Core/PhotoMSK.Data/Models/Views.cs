using System;
using System.Data.Entity.ModelConfiguration;
using PhotoMSK.Data.Enums;

namespace PhotoMSK.Data.Models
{
    public class Views
    {
        public virtual Guid ID { get; set; }
        public virtual WallPost WallPost { get; set; }
        public Guid EntityID { get; set; }
        public virtual RouteEntity Entity { get; set; }
        public ParticipationType ParticipationType { get; set; }
        public DateTime Date { get; set; }

        public class ViewConfigurator : EntityTypeConfiguration<Views>
        {
            public ViewConfigurator()
            {
                HasRequired(x => x.Entity).WithMany(x => x.Wall).HasForeignKey(x => x.EntityID);
            }
        }
    }
}