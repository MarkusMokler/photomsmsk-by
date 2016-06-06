using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class RouteSite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RouteEntity", "Site", c => c.String());
            DropColumn("dbo.Photostudio", "Site");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photostudio", "Site", c => c.String());
            DropColumn("dbo.RouteEntity", "Site");
        }
    }
}
