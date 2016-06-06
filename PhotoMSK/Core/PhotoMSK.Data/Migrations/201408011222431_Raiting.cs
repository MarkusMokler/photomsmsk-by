using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class Raiting : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RouteEntity", newName: "RouteEntities");
            CreateTable(
                "dbo.Raitings",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Views = c.Int(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                        WallPosts = c.Int(nullable: false),
                        Comments = c.Int(nullable: false),
                        Payed = c.Int(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntities", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Raitings", "ID", "dbo.RouteEntities");
            DropIndex("dbo.Raitings", new[] { "ID" });
            DropTable("dbo.Raitings");
            RenameTable(name: "dbo.RouteEntities", newName: "RouteEntity");
        }
    }
}
