using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class UpAllowOnlineBookingToRoute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RouteEntity", "AllowOnlineBooking", c => c.Boolean(nullable: false));
            DropColumn("dbo.Photostudio", "AllowOnlineBooking");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photostudio", "AllowOnlineBooking", c => c.Boolean(nullable: false));
            DropColumn("dbo.RouteEntity", "AllowOnlineBooking");
        }
    }
}
