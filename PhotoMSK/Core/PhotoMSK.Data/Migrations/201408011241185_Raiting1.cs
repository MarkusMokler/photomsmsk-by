using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class Raiting1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Raitings", new[] { "ID" });
            CreateIndex("dbo.RouteEntities", "ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RouteEntities", new[] { "ID" });
            CreateIndex("dbo.Raitings", "ID");
        }
    }
}
