using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class MenuItems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AbstractMenuItem",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RouteID = c.Guid(nullable: false),
                        Name = c.String(),
                        Order = c.String(),
                        Publish = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.RouteID, cascadeDelete: true)
                .Index(t => t.RouteID);
            
            CreateTable(
                "dbo.LinkMenuItem",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AbstractMenuItem", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.PageMenuItem",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        TextPageID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AbstractMenuItem", t => t.ID)
                .ForeignKey("dbo.TextPages", t => t.TextPageID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TextPageID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PageMenuItem", "TextPageID", "dbo.TextPages");
            DropForeignKey("dbo.PageMenuItem", "ID", "dbo.AbstractMenuItem");
            DropForeignKey("dbo.LinkMenuItem", "ID", "dbo.AbstractMenuItem");
            DropForeignKey("dbo.AbstractMenuItem", "RouteID", "dbo.RouteEntity");
            DropIndex("dbo.PageMenuItem", new[] { "TextPageID" });
            DropIndex("dbo.PageMenuItem", new[] { "ID" });
            DropIndex("dbo.LinkMenuItem", new[] { "ID" });
            DropIndex("dbo.AbstractMenuItem", new[] { "RouteID" });
            DropTable("dbo.PageMenuItem");
            DropTable("dbo.LinkMenuItem");
            DropTable("dbo.AbstractMenuItem");
        }
    }
}
