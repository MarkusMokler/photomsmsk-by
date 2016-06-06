using System;
using System.Data.Entity.ModelConfiguration;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.Data.Models
{
    public class ShopCategory : Category
    {
        public Photoshop Photoshop { get; set; }
        public Guid PhotoshopID { get; set; }
        public int Order { get; set; }
    }

    public class ShopCategoryConfiguration : EntityTypeConfiguration<ShopCategory>
    {
        public ShopCategoryConfiguration()
        {
            ToTable("ShopCategory");
            HasRequired(x => x.Photoshop).WithMany(x => x.Categories).HasForeignKey(x => x.PhotoshopID);
        }
    }
}