namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WidgetsAndFiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LandingPage",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BaseWidget",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Position = c.Int(nullable: false),
                        LandingPageID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LandingPage", t => t.LandingPageID, cascadeDelete: true)
                .Index(t => t.LandingPageID);
            
            CreateTable(
                "dbo.FileEntry",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ParentEntryID = c.Guid(),
                        Type = c.Int(nullable: false),
                        AttachmentID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Attachments", t => t.AttachmentID)
                .ForeignKey("dbo.FileEntry", t => t.ParentEntryID)
                .Index(t => t.ParentEntryID)
                .Index(t => t.AttachmentID);
            
            CreateTable(
                "dbo.VideoWidget",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        VideoID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Videos", t => t.VideoID)
                .Index(t => t.VideoID);
            
            CreateTable(
                "dbo.QuoteWidget",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Text = c.String(),
                        Autor = c.String(),
                        AutorLink = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BaseWidget", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.TextAdnDescriptionWidget",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PhotoID = c.Guid(),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BaseWidget", t => t.ID)
                .ForeignKey("dbo.Photos", t => t.PhotoID)
                .Index(t => t.ID)
                .Index(t => t.PhotoID);
            
            AddColumn("dbo.RouteEntity", "RootDirectoryID", c => c.Guid());
            AddColumn("dbo.Halls", "LandingPageID", c => c.Guid());
            CreateIndex("dbo.RouteEntity", "RootDirectoryID");
            CreateIndex("dbo.Halls", "LandingPageID");
            AddForeignKey("dbo.Halls", "LandingPageID", "dbo.LandingPage", "ID");
            AddForeignKey("dbo.RouteEntity", "RootDirectoryID", "dbo.FileEntry", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TextAdnDescriptionWidget", "PhotoID", "dbo.Photos");
            DropForeignKey("dbo.TextAdnDescriptionWidget", "ID", "dbo.BaseWidget");
            DropForeignKey("dbo.QuoteWidget", "ID", "dbo.BaseWidget");
            DropForeignKey("dbo.VideoWidget", "VideoID", "dbo.Videos");
            DropForeignKey("dbo.RouteEntity", "RootDirectoryID", "dbo.FileEntry");
            DropForeignKey("dbo.Halls", "LandingPageID", "dbo.LandingPage");
            DropForeignKey("dbo.FileEntry", "ParentEntryID", "dbo.FileEntry");
            DropForeignKey("dbo.FileEntry", "AttachmentID", "dbo.Attachments");
            DropForeignKey("dbo.BaseWidget", "LandingPageID", "dbo.LandingPage");
            DropIndex("dbo.TextAdnDescriptionWidget", new[] { "PhotoID" });
            DropIndex("dbo.TextAdnDescriptionWidget", new[] { "ID" });
            DropIndex("dbo.QuoteWidget", new[] { "ID" });
            DropIndex("dbo.VideoWidget", new[] { "VideoID" });
            DropIndex("dbo.FileEntry", new[] { "AttachmentID" });
            DropIndex("dbo.FileEntry", new[] { "ParentEntryID" });
            DropIndex("dbo.BaseWidget", new[] { "LandingPageID" });
            DropIndex("dbo.Halls", new[] { "LandingPageID" });
            DropIndex("dbo.RouteEntity", new[] { "RootDirectoryID" });
            DropColumn("dbo.Halls", "LandingPageID");
            DropColumn("dbo.RouteEntity", "RootDirectoryID");
            DropTable("dbo.TextAdnDescriptionWidget");
            DropTable("dbo.QuoteWidget");
            DropTable("dbo.VideoWidget");
            DropTable("dbo.FileEntry");
            DropTable("dbo.BaseWidget");
            DropTable("dbo.LandingPage");
        }
    }
}
