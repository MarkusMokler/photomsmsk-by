using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class CopyPublics : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CopyReferences",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CopyFrom = c.Int(nullable: false),
                        LastCopiedID = c.Int(nullable: false),
                        LastCollectTime = c.DateTime(nullable: false),
                        RouteID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.RouteID, cascadeDelete: true)
                .Index(t => t.RouteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CopyReferences", "RouteID", "dbo.RouteEntity");
            DropIndex("dbo.CopyReferences", new[] { "RouteID" });
            DropTable("dbo.CopyReferences");
        }
    }
}
