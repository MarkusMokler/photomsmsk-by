using System;
using System.Data.Entity.ModelConfiguration;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.Data.Models
{
    public class CategoryBrand
    {
        public Guid ID { get; set; }
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public Guid CategoryID { get; set; }
        public Guid BrandID { get; set; }
    }

    public class CategoryBrandConfiguration : EntityTypeConfiguration<CategoryBrand>
    {
        public CategoryBrandConfiguration()
        {
            ToTable("CategoryBrand");
            HasRequired(x => x.Brand).WithMany(x => x.Categories).HasForeignKey(x => x.BrandID);
            HasRequired(x => x.Category).WithMany(x => x.Brands).HasForeignKey(x => x.CategoryID);
        }
    }
}