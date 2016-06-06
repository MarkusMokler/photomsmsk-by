using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class TextPages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TextPages",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RouteID = c.Guid(nullable: false),
                        Title = c.String(),
                        Text = c.String(),
                        Keywords = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.RouteID, cascadeDelete: true)
                .Index(t => t.RouteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TextPages", "RouteID", "dbo.RouteEntity");
            DropIndex("dbo.TextPages", new[] { "RouteID" });
            DropTable("dbo.TextPages");
        }
    }
}
