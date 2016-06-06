namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContentPage",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BasePages", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.LandingPage",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BasePages", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.ProjectionPage",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ChildPageCategoryID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BasePages", t => t.ID)
                .ForeignKey("dbo.PageCategories", t => t.ChildPageCategoryID)
                .Index(t => t.ID)
                .Index(t => t.ChildPageCategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectionPage", "ChildPageCategoryID", "dbo.PageCategories");
            DropForeignKey("dbo.ProjectionPage", "ID", "dbo.BasePages");
            DropForeignKey("dbo.LandingPage", "ID", "dbo.BasePages");
            DropForeignKey("dbo.ContentPage", "ID", "dbo.BasePages");
            DropIndex("dbo.ProjectionPage", new[] { "ChildPageCategoryID" });
            DropIndex("dbo.ProjectionPage", new[] { "ID" });
            DropIndex("dbo.LandingPage", new[] { "ID" });
            DropIndex("dbo.ContentPage", new[] { "ID" });
            DropTable("dbo.ProjectionPage");
            DropTable("dbo.LandingPage");
            DropTable("dbo.ContentPage");
        }
    }
}
