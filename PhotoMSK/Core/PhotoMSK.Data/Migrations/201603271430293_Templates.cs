namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Templates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HtmlTemplates",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ThemeName = c.String(),
                        ThemeType = c.String(),
                        ViewName = c.String(),
                        Content = c.String(),
                        RouteID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.RouteID)
                .Index(t => t.RouteID);
            
            DropTable("dbo.DbRazorViews");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DbRazorViews",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ThemeName = c.String(),
                        ThemeType = c.String(),
                        ViewName = c.String(),
                        RazorContent = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.HtmlTemplates", "RouteID", "dbo.RouteEntity");
            DropIndex("dbo.HtmlTemplates", new[] { "RouteID" });
            DropTable("dbo.HtmlTemplates");
        }
    }
}
