using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models.Menu
{
    public class LinkMenuItem : AbstractMenuItem
    {
        public string Link { get; set; }
    }
    public class LinkMenuItemConfiguration : EntityTypeConfiguration<LinkMenuItem>
    {
        public LinkMenuItemConfiguration()
        {
            ToTable("LinkMenuItem");
        }
    }
}