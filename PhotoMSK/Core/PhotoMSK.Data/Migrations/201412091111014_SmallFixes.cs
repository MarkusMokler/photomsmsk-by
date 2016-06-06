using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class SmallFixes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RouteEntity", "CreationTime", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AddColumn("dbo.Photoshops", "CurencyValue", c => c.Double(nullable: false));
            AddColumn("dbo.Categories", "Slug", c => c.String());
            AddColumn("dbo.PricePositions", "Vitrine", c => c.Double(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.PricePositions", "Vitrine");
            DropColumn("dbo.Categories", "Slug");
            DropColumn("dbo.Photoshops", "CurencyValue");
            DropColumn("dbo.RouteEntity", "CreationTime");
        }
    }
}
