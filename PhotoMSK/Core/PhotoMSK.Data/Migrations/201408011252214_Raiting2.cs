using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class Raiting2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RouteEntities", newName: "RouteEntity");
            DropIndex("dbo.RouteEntity", new[] { "ID" });
            CreateIndex("dbo.Raitings", "ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Raitings", new[] { "ID" });
            CreateIndex("dbo.RouteEntity", "ID");
            RenameTable(name: "dbo.RouteEntity", newName: "RouteEntities");
        }
    }
}
