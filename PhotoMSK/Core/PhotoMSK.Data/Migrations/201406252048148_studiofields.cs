using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class studiofields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RouteEntity", "TeaserImage", c => c.String());
            AddColumn("dbo.RouteEntity", "CoverImage", c => c.String());
            AddColumn("dbo.Halls", "TeaserImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Halls", "TeaserImage");
            DropColumn("dbo.RouteEntity", "CoverImage");
            DropColumn("dbo.RouteEntity", "TeaserImage");
        }
    }
}
