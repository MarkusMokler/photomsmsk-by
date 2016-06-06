namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MainPage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RouteEntity", "MainPageID", c => c.Guid());
            AddColumn("dbo.RouteEntity", "DefaultRoutePageLayoutID", c => c.Guid());
            CreateIndex("dbo.RouteEntity", "MainPageID");
            CreateIndex("dbo.RouteEntity", "DefaultRoutePageLayoutID");
            AddForeignKey("dbo.RouteEntity", "DefaultRoutePageLayoutID", "dbo.RoutepageLayouts", "ID");
            AddForeignKey("dbo.RouteEntity", "MainPageID", "dbo.BasePages", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RouteEntity", "MainPageID", "dbo.BasePages");
            DropForeignKey("dbo.RouteEntity", "DefaultRoutePageLayoutID", "dbo.RoutepageLayouts");
            DropIndex("dbo.RouteEntity", new[] { "DefaultRoutePageLayoutID" });
            DropIndex("dbo.RouteEntity", new[] { "MainPageID" });
            DropColumn("dbo.RouteEntity", "DefaultRoutePageLayoutID");
            DropColumn("dbo.RouteEntity", "MainPageID");
        }
    }
}
