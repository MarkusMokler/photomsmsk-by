namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientTags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RouteClientCategories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RouteID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        TagID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.RouteID)
                .ForeignKey("dbo.Tags", t => t.TagID)
                .ForeignKey("dbo.UserInformations", t => t.UserID)
                .Index(t => t.RouteID)
                .Index(t => t.UserID)
                .Index(t => t.TagID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RouteClientCategories", "UserID", "dbo.UserInformations");
            DropForeignKey("dbo.RouteClientCategories", "TagID", "dbo.Tags");
            DropForeignKey("dbo.RouteClientCategories", "RouteID", "dbo.RouteEntity");
            DropIndex("dbo.RouteClientCategories", new[] { "TagID" });
            DropIndex("dbo.RouteClientCategories", new[] { "UserID" });
            DropIndex("dbo.RouteClientCategories", new[] { "RouteID" });
            DropTable("dbo.Tags");
            DropTable("dbo.RouteClientCategories");
        }
    }
}
