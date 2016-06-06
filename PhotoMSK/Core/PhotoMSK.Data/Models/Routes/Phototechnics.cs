using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models.Routes
{
    public class Phototechnics : RouteEntity
    {
        public string Format { get; set; }
        public virtual Category Category { get; set; }
        public Guid? CategoryID { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Guid? BrandID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ParameterValue> ParameterValues { get; set; }
        public virtual ICollection<PricePosition> Prices { get; set; }
        public virtual ICollection<RentCalendar> Rents { get; set; }
        public override string GetName()
        {
            return Name;
        }
    }

    public class PhototechnicsConfiguration : EntityTypeConfiguration<Phototechnics>
    {
        public PhototechnicsConfiguration()
        {
            ToTable("Phototechnics");
            HasOptional(x => x.Brand).WithMany(x => x.Phototechnics).HasForeignKey(x => x.BrandID);
            HasOptional(x => x.Category).WithMany(x => x.Phototechnics).HasForeignKey(x => x.CategoryID);
        }
    }
}