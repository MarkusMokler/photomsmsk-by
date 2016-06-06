using System;
using System.Data.Entity.ModelConfiguration;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.Data.Models
{
    public class PricePosition
    {
        public Guid ID { get; set; }
        public virtual Guid PhotoshopID { get; set; }
        public virtual Photoshop Photoshop { get; set; }
        public virtual Guid PhototechnicsID { get; set; }
        public virtual Phototechnics Phototechnics { get; set; }
        public double Vitrine { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public string Warranty { get; set; }
        public string Delivery { get; set; }
        public string IsCashless { get; set; }
        public string IsCredit { get; set; }
        public int BarCode { get; set; }
    }

    public class PricePositionConfigurator : EntityTypeConfiguration<PricePosition>
    {
        public PricePositionConfigurator()
        {
            HasKey(x => x.ID);
            HasRequired(x => x.Photoshop).WithMany(photoshop => photoshop.Positions).HasForeignKey(x => x.PhotoshopID);
            HasRequired(x => x.Phototechnics).WithMany(phototechnics => phototechnics.Prices).HasForeignKey(x => x.PhototechnicsID);
        }
    }
}