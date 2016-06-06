using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class PageCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PageCategories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RouteID = c.Guid(nullable: false),
                        Slug = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.RouteID, cascadeDelete: true)
                .Index(t => t.RouteID);
            
            AddColumn("dbo.TextPages", "Slug", c => c.String());
            AddColumn("dbo.TextPages", "Comment", c => c.Boolean(nullable: false));
            AddColumn("dbo.TextPages", "PageCategoryID", c => c.Guid(nullable: false));
            AddColumn("dbo.TextPages", "UserInformationID", c => c.Guid(nullable: false));
            CreateIndex("dbo.TextPages", "PageCategoryID");
            CreateIndex("dbo.TextPages", "UserInformationID");
            AddForeignKey("dbo.TextPages", "PageCategoryID", "dbo.PageCategories", "ID");
            AddForeignKey("dbo.TextPages", "UserInformationID", "dbo.UserInformations", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PageCategories", "RouteID", "dbo.RouteEntity");
            DropForeignKey("dbo.TextPages", "UserInformationID", "dbo.UserInformations");
            DropForeignKey("dbo.TextPages", "PageCategoryID", "dbo.PageCategories");
            DropIndex("dbo.TextPages", new[] { "UserInformationID" });
            DropIndex("dbo.TextPages", new[] { "PageCategoryID" });
            DropIndex("dbo.PageCategories", new[] { "RouteID" });
            DropColumn("dbo.TextPages", "UserInformationID");
            DropColumn("dbo.TextPages", "PageCategoryID");
            DropColumn("dbo.TextPages", "Comment");
            DropColumn("dbo.TextPages", "Slug");
            DropTable("dbo.PageCategories");
        }
    }
}
