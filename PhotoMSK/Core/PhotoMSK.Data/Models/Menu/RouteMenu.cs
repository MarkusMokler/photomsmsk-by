using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models.Menu
{
    public class RouteMenu : AbstractMenuItem
    {

    }

    public class RouteMenuItemConfiguration : EntityTypeConfiguration<RouteMenu>
    {
        public RouteMenuItemConfiguration()
        {
            ToTable("RouteMenu");
        }
    }
}