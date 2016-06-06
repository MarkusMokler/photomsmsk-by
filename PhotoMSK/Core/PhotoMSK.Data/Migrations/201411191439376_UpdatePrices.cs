using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class UpdatePrices : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PricePositions", new[] { "Photoshop_ID" });
            DropIndex("dbo.PricePositions", new[] { "Phototechnics_ID" });
            DropColumn("dbo.PricePositions", "Phototechnics_ID");
            DropColumn("dbo.PricePositions", "Photoshop_ID");
        }

        public override void Down()
        {
            CreateIndex("dbo.PricePositions", "Phototechnics_ID");
            CreateIndex("dbo.PricePositions", "Photoshop_ID");
            CreateIndex("dbo.PricePositions", "Phototechnics_ID");
            CreateIndex("dbo.PricePositions", "Photoshop_ID");
        }
    }
}
