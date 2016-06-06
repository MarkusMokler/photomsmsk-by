using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class UpdateRoute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RouteEntity", "Facebook", c => c.String());
            AddColumn("dbo.RouteEntity", "Vk", c => c.String());
            AddColumn("dbo.RouteEntity", "Instagram", c => c.String());
            AddColumn("dbo.RouteEntity", "Twitter", c => c.String());
            AddColumn("dbo.RouteEntity", "Skype", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RouteEntity", "Skype");
            DropColumn("dbo.RouteEntity", "Twitter");
            DropColumn("dbo.RouteEntity", "Instagram");
            DropColumn("dbo.RouteEntity", "Vk");
            DropColumn("dbo.RouteEntity", "Facebook");
        }
    }
}
