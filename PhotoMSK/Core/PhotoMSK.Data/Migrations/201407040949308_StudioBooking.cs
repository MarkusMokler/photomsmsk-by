using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class StudioBooking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RouteEntity", "PageViews", c => c.Int(nullable: false));
            AddColumn("dbo.Photostudio", "AllowOnlineBooking", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photostudio", "AllowOnlineBooking");
            DropColumn("dbo.RouteEntity", "PageViews");
        }
    }
}
