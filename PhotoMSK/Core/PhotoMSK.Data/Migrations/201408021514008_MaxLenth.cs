using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class MaxLenth : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RouteEntity", new[] { "Shortcut" });
            AlterColumn("dbo.RouteEntity", "Shortcut", c => c.String(maxLength: 35));
            CreateIndex("dbo.RouteEntity", "Shortcut", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.RouteEntity", new[] { "Shortcut" });
            AlterColumn("dbo.RouteEntity", "Shortcut", c => c.String(maxLength: 25));
            CreateIndex("dbo.RouteEntity", "Shortcut", unique: true);
        }
    }
}
