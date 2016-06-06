using System;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models
{
    public abstract class BasePage
    {
        public Guid ID { get; set; }
        public Guid RouteID { get; set; }
        public virtual RouteEntity Route { get; set; }

        public string Title { get; set; }
        public string Slug { get; set; }
        public string Text { get; set; }

        public string Keywords { get; set; }
        public string Description { get; set; }

        public bool Published { get; set; }
        public bool Comment { get; set; }

        public string PageType { get; set; }

        public Guid PageCategoryID { get; set; }
        public virtual PageCategory PageCategory { get; set; }

        public Guid? PageLayoutID { get; set; }
        public virtual RoutepageLayout PageLayout { get; set; }
        public Guid UserInformationID { get; set; }
        public virtual UserInformation UserInformation { get; set; }
    }

    public class TextPageConfiguration : EntityTypeConfiguration<BasePage>
    {
        public TextPageConfiguration()
        {
            HasKey(x => x.ID);
            HasRequired(x => x.Route).WithMany(x => x.Pages).HasForeignKey(x => x.RouteID);
            HasRequired(x => x.PageCategory).WithMany(x => x.Pages).HasForeignKey(x => x.PageCategoryID);
            HasRequired(x => x.UserInformation).WithMany().HasForeignKey(x => x.UserInformationID);
        }
    }
}