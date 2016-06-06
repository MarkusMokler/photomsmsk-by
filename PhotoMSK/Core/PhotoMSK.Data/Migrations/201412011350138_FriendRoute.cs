using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class FriendRoute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RouteEntity", "FriendlyRoute", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RouteEntity", "FriendlyRoute");
        }
    }
}
