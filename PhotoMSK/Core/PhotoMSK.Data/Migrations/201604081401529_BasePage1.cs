namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasePage1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuWidget",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        MenuID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BaseWidget", t => t.ID)
                .ForeignKey("dbo.RouteMenu", t => t.MenuID)
                .Index(t => t.ID)
                .Index(t => t.MenuID);
            
            CreateTable(
                "dbo.CategoryProjectionMenuItem",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PageCategoryID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AbstractMenuItem", t => t.ID)
                .ForeignKey("dbo.PageCategories", t => t.PageCategoryID)
                .Index(t => t.ID)
                .Index(t => t.PageCategoryID);
            
            CreateTable(
                "dbo.HallsMenuItem",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        HallID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AbstractMenuItem", t => t.ID)
                .ForeignKey("dbo.Halls", t => t.HallID)
                .Index(t => t.ID)
                .Index(t => t.HallID);
            
            CreateTable(
                "dbo.RouteMenu",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AbstractMenuItem", t => t.ID)
                .Index(t => t.ID);
            
            AddColumn("dbo.AbstractMenuItem", "ParentID", c => c.Guid());
            CreateIndex("dbo.AbstractMenuItem", "ParentID");
            AddForeignKey("dbo.AbstractMenuItem", "ParentID", "dbo.AbstractMenuItem", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RouteMenu", "ID", "dbo.AbstractMenuItem");
            DropForeignKey("dbo.HallsMenuItem", "HallID", "dbo.Halls");
            DropForeignKey("dbo.HallsMenuItem", "ID", "dbo.AbstractMenuItem");
            DropForeignKey("dbo.CategoryProjectionMenuItem", "PageCategoryID", "dbo.PageCategories");
            DropForeignKey("dbo.CategoryProjectionMenuItem", "ID", "dbo.AbstractMenuItem");
            DropForeignKey("dbo.MenuWidget", "MenuID", "dbo.RouteMenu");
            DropForeignKey("dbo.MenuWidget", "ID", "dbo.BaseWidget");
            DropForeignKey("dbo.AbstractMenuItem", "ParentID", "dbo.AbstractMenuItem");
            DropIndex("dbo.RouteMenu", new[] { "ID" });
            DropIndex("dbo.HallsMenuItem", new[] { "HallID" });
            DropIndex("dbo.HallsMenuItem", new[] { "ID" });
            DropIndex("dbo.CategoryProjectionMenuItem", new[] { "PageCategoryID" });
            DropIndex("dbo.CategoryProjectionMenuItem", new[] { "ID" });
            DropIndex("dbo.MenuWidget", new[] { "MenuID" });
            DropIndex("dbo.MenuWidget", new[] { "ID" });
            DropIndex("dbo.AbstractMenuItem", new[] { "ParentID" });
            DropColumn("dbo.AbstractMenuItem", "ParentID");
            DropTable("dbo.RouteMenu");
            DropTable("dbo.HallsMenuItem");
            DropTable("dbo.CategoryProjectionMenuItem");
            DropTable("dbo.MenuWidget");
        }
    }
}
