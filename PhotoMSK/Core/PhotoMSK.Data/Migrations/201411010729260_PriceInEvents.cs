using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class PriceInEvents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "CreaTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "DaylyPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Events", "Tags", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Tags");
            DropColumn("dbo.Events", "DaylyPrice");
            DropColumn("dbo.Events", "CreaTime");
        }
    }
}
