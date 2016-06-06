using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class updateRoutes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RouteEntity", "SeoTitle", c => c.String());
            AddColumn("dbo.RouteEntity", "SeoDescription", c => c.String());
            AddColumn("dbo.RouteEntity", "SeoKeywords", c => c.String());
            AddColumn("dbo.RouteEntity", "WhiteLabel", c => c.Boolean(nullable: false));
            AddColumn("dbo.RouteEntity", "Domain", c => c.String());
            AddColumn("dbo.RouteEntity", "Theme", c => c.String());
            AddColumn("dbo.Masterclass", "StartDay", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Masterclass", "StartDay");
            DropColumn("dbo.RouteEntity", "Theme");
            DropColumn("dbo.RouteEntity", "Domain");
            DropColumn("dbo.RouteEntity", "WhiteLabel");
            DropColumn("dbo.RouteEntity", "SeoKeywords");
            DropColumn("dbo.RouteEntity", "SeoDescription");
            DropColumn("dbo.RouteEntity", "SeoTitle");
        }
    }
}
