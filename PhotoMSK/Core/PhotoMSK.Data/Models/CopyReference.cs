using System;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models
{
    public class CopyReference
    {
        public Guid ID { get; set; }
        public int CopyFrom { get; set; }
        public int LastCopiedID { get; set; }
        public DateTime LastCollectTime { get; set; }
        public virtual Guid RouteID { get; set; }
        public virtual RouteEntity Route { get; set; }

        public static CopyReference New()
        {
            return new CopyReference {ID = Guid.NewGuid(), LastCollectTime = DateTime.Now.AddDays(-1)};
        }
    }

    public class CopyReferenceConfigurator : EntityTypeConfiguration<CopyReference>
    {
        public CopyReferenceConfigurator()
        {
            HasKey(x => x.ID);
            HasRequired(x => x.Route).WithMany().HasForeignKey(x => x.RouteID);
        }
    }
}