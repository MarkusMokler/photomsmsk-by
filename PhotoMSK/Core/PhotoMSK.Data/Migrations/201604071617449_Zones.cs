namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zones : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LandingPage", newName: "Zones");
            RenameColumn(table: "dbo.BaseWidget", name: "LandingPageID", newName: "ZoneID");
            RenameColumn(table: "dbo.Halls", name: "LandingPageID", newName: "ZoneID");
            RenameIndex(table: "dbo.Halls", name: "IX_LandingPageID", newName: "IX_ZoneID");
            RenameIndex(table: "dbo.BaseWidget", name: "IX_LandingPageID", newName: "IX_ZoneID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BaseWidget", name: "IX_ZoneID", newName: "IX_LandingPageID");
            RenameIndex(table: "dbo.Halls", name: "IX_ZoneID", newName: "IX_LandingPageID");
            RenameColumn(table: "dbo.Halls", name: "ZoneID", newName: "LandingPageID");
            RenameColumn(table: "dbo.BaseWidget", name: "ZoneID", newName: "LandingPageID");
            RenameTable(name: "dbo.Zones", newName: "LandingPage");
        }
    }
}
