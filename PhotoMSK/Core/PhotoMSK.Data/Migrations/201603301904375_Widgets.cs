namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Widgets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionAnswers",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Question = c.String(),
                        Answer = c.String(),
                        FaqWidget_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FaqWidget", t => t.FaqWidget_ID)
                .Index(t => t.FaqWidget_ID);
            
            CreateTable(
                "dbo.FileGalleries",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        FileEntryID = c.Guid(nullable: false),
                        GalleryWidgetID = c.Guid(nullable: false),
                        Position = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileEntry", t => t.FileEntryID)
                .ForeignKey("dbo.GalleryWidget", t => t.GalleryWidgetID)
                .Index(t => t.FileEntryID)
                .Index(t => t.GalleryWidgetID);
            
            CreateTable(
                "dbo.FaqWidget",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BaseWidget", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.SplitWidget",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        LeftID = c.Guid(nullable: false),
                        RightID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BaseWidget", t => t.ID)
                .ForeignKey("dbo.BaseWidget", t => t.LeftID)
                .ForeignKey("dbo.BaseWidget", t => t.RightID)
                .Index(t => t.ID)
                .Index(t => t.LeftID)
                .Index(t => t.RightID);
            
            CreateTable(
                "dbo.HeaderWidget",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BaseWidget", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.DescriptionWidget",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BaseWidget", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.GalleryWidget",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BaseWidget", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.ContainerWidget",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        WidgetID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BaseWidget", t => t.ID)
                .ForeignKey("dbo.BaseWidget", t => t.WidgetID)
                .Index(t => t.ID)
                .Index(t => t.WidgetID);
            
            AddColumn("dbo.Photos", "SmallUrl", c => c.String());
            AddColumn("dbo.Photos", "MediumUrl", c => c.String());
            AddColumn("dbo.Photos", "LargeUrl", c => c.String());
            CreateIndex("dbo.VideoWidget", "ID");
            AddForeignKey("dbo.VideoWidget", "ID", "dbo.BaseWidget", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoWidget", "ID", "dbo.BaseWidget");
            DropForeignKey("dbo.ContainerWidget", "WidgetID", "dbo.BaseWidget");
            DropForeignKey("dbo.ContainerWidget", "ID", "dbo.BaseWidget");
            DropForeignKey("dbo.GalleryWidget", "ID", "dbo.BaseWidget");
            DropForeignKey("dbo.DescriptionWidget", "ID", "dbo.BaseWidget");
            DropForeignKey("dbo.HeaderWidget", "ID", "dbo.BaseWidget");
            DropForeignKey("dbo.SplitWidget", "RightID", "dbo.BaseWidget");
            DropForeignKey("dbo.SplitWidget", "LeftID", "dbo.BaseWidget");
            DropForeignKey("dbo.SplitWidget", "ID", "dbo.BaseWidget");
            DropForeignKey("dbo.FaqWidget", "ID", "dbo.BaseWidget");
            DropForeignKey("dbo.FileGalleries", "GalleryWidgetID", "dbo.GalleryWidget");
            DropForeignKey("dbo.FileGalleries", "FileEntryID", "dbo.FileEntry");
            DropForeignKey("dbo.QuestionAnswers", "FaqWidget_ID", "dbo.FaqWidget");
            DropIndex("dbo.VideoWidget", new[] { "ID" });
            DropIndex("dbo.ContainerWidget", new[] { "WidgetID" });
            DropIndex("dbo.ContainerWidget", new[] { "ID" });
            DropIndex("dbo.GalleryWidget", new[] { "ID" });
            DropIndex("dbo.DescriptionWidget", new[] { "ID" });
            DropIndex("dbo.HeaderWidget", new[] { "ID" });
            DropIndex("dbo.SplitWidget", new[] { "RightID" });
            DropIndex("dbo.SplitWidget", new[] { "LeftID" });
            DropIndex("dbo.SplitWidget", new[] { "ID" });
            DropIndex("dbo.FaqWidget", new[] { "ID" });
            DropIndex("dbo.FileGalleries", new[] { "GalleryWidgetID" });
            DropIndex("dbo.FileGalleries", new[] { "FileEntryID" });
            DropIndex("dbo.QuestionAnswers", new[] { "FaqWidget_ID" });
            DropColumn("dbo.Photos", "LargeUrl");
            DropColumn("dbo.Photos", "MediumUrl");
            DropColumn("dbo.Photos", "SmallUrl");
            DropTable("dbo.ContainerWidget");
            DropTable("dbo.GalleryWidget");
            DropTable("dbo.DescriptionWidget");
            DropTable("dbo.HeaderWidget");
            DropTable("dbo.SplitWidget");
            DropTable("dbo.FaqWidget");
            DropTable("dbo.FileGalleries");
            DropTable("dbo.QuestionAnswers");
        }
    }
}
