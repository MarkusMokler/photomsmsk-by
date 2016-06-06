namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Project : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        PhotographerID = c.Guid(nullable: false),
                        Price = c.Int(nullable: false),
                        Description = c.String(),
                        LandingPageUrl = c.String(),
                        Type = c.String(),
                        TeaserImage = c.String(),
                        CoverImage = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Photographer", t => t.PhotographerID)
                .Index(t => t.PhotographerID);
            
            CreateTable(
                "dbo.ProjectSettings",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProjectID = c.Guid(nullable: false),
                        Value = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectID);
            
            AddColumn("dbo.RoutepageLayouts", "RouteID", c => c.Guid(nullable: false));
            CreateIndex("dbo.RoutepageLayouts", "RouteID");
            AddForeignKey("dbo.RoutepageLayouts", "RouteID", "dbo.RouteEntity", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectSettings", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Projects", "PhotographerID", "dbo.Photographer");
            DropForeignKey("dbo.RoutepageLayouts", "RouteID", "dbo.RouteEntity");
            DropIndex("dbo.ProjectSettings", new[] { "ProjectID" });
            DropIndex("dbo.Projects", new[] { "PhotographerID" });
            DropIndex("dbo.RoutepageLayouts", new[] { "RouteID" });
            DropColumn("dbo.RoutepageLayouts", "RouteID");
            DropTable("dbo.ProjectSettings");
            DropTable("dbo.Projects");
        }
    }
}
