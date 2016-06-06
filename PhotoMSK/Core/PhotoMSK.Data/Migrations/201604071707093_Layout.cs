namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Layout : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoutepageLayouts",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        LayoutID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Layouts", t => t.LayoutID, cascadeDelete: true)
                .Index(t => t.LayoutID);
            
            CreateTable(
                "dbo.Layouts",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Content = c.String(),
                        Zones = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.BasePages", "PageLayoutID", c => c.Guid());
            AddColumn("dbo.Zones", "Name", c => c.String());
            AddColumn("dbo.Zones", "LayoutID", c => c.Guid());
            CreateIndex("dbo.BasePages", "PageLayoutID");
            CreateIndex("dbo.Zones", "LayoutID");
            AddForeignKey("dbo.Zones", "LayoutID", "dbo.RoutepageLayouts", "ID");
            AddForeignKey("dbo.BasePages", "PageLayoutID", "dbo.RoutepageLayouts", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasePages", "PageLayoutID", "dbo.RoutepageLayouts");
            DropForeignKey("dbo.Zones", "LayoutID", "dbo.RoutepageLayouts");
            DropForeignKey("dbo.RoutepageLayouts", "LayoutID", "dbo.Layouts");
            DropIndex("dbo.Zones", new[] { "LayoutID" });
            DropIndex("dbo.RoutepageLayouts", new[] { "LayoutID" });
            DropIndex("dbo.BasePages", new[] { "PageLayoutID" });
            DropColumn("dbo.Zones", "LayoutID");
            DropColumn("dbo.Zones", "Name");
            DropColumn("dbo.BasePages", "PageLayoutID");
            DropTable("dbo.Layouts");
            DropTable("dbo.RoutepageLayouts");
        }
    }
}
