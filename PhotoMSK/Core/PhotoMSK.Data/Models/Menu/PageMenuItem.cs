using System;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace PhotoMSK.Data.Models.Menu
{
    public class PageMenuItem : AbstractMenuItem
    {
        public Guid TextPageID { get; set; }
        public BasePage BasePage { get; set; }

        public BasePage GetPage()
        {
            return BasePage;
        }
    }

    public class PageMenuItemConfiguration : EntityTypeConfiguration<PageMenuItem>
    {
        public PageMenuItemConfiguration()
        {
            ToTable("PageMenuItem");

            HasRequired(x => x.BasePage).WithMany().HasForeignKey(x => x.TextPageID);
        }
    }
}