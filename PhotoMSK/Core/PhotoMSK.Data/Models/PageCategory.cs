using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models
{
    public class PageCategory
    {
        public Guid ID { get; set; }
        public Guid RouteID { get; set; }
        public virtual RouteEntity Route { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public ICollection<BasePage> Pages { get; set; }
    }

    public class PageCategoryConfiguration : EntityTypeConfiguration<PageCategory>
    {
        public PageCategoryConfiguration()
        {
            HasKey(x => x.ID);
            HasRequired(x => x.Route).WithMany(x => x.PageCategories).HasForeignKey(x => x.RouteID);
        }
    }

}