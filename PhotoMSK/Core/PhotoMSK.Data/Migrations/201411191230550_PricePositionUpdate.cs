using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class PricePositionUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PricePositions", "Photoshop_ID", "dbo.Photoshops");
            DropForeignKey("dbo.PricePositions", "Phototechnics_ID", "dbo.Phototechnics");
            AddColumn("dbo.PricePositions", "PhotoshopID", c => c.Guid(nullable: false));
            AddColumn("dbo.PricePositions", "PhototechnicsID", c => c.Guid(nullable: false));
            AddColumn("dbo.PricePositions", "Currency", c => c.String());
            AddColumn("dbo.PricePositions", "Status", c => c.String());
            AddColumn("dbo.PricePositions", "Warranty", c => c.String());
            AddColumn("dbo.PricePositions", "Delivery", c => c.String());
            AddColumn("dbo.PricePositions", "IsCashless", c => c.String());
            AddColumn("dbo.PricePositions", "IsCredit", c => c.String());
            CreateIndex("dbo.PricePositions", "PhotoshopID");
            CreateIndex("dbo.PricePositions", "PhototechnicsID");
            AddForeignKey("dbo.PricePositions", "PhotoshopID", "dbo.Photoshops", "ID");
            AddForeignKey("dbo.PricePositions", "PhototechnicsID", "dbo.Phototechnics", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PricePositions", "PhototechnicsID", "dbo.Phototechnics");
            DropForeignKey("dbo.PricePositions", "PhotoshopID", "dbo.Photoshops");
            DropIndex("dbo.PricePositions", new[] { "PhototechnicsID" });
            DropIndex("dbo.PricePositions", new[] { "PhotoshopID" });
            DropColumn("dbo.PricePositions", "IsCredit");
            DropColumn("dbo.PricePositions", "IsCashless");
            DropColumn("dbo.PricePositions", "Delivery");
            DropColumn("dbo.PricePositions", "Warranty");
            DropColumn("dbo.PricePositions", "Status");
            DropColumn("dbo.PricePositions", "Currency");
            DropColumn("dbo.PricePositions", "PhototechnicsID");
            DropColumn("dbo.PricePositions", "PhotoshopID");
            AddForeignKey("dbo.PricePositions", "Phototechnics_ID", "dbo.Phototechnics", "ID");
            AddForeignKey("dbo.PricePositions", "Photoshop_ID", "dbo.Photoshops", "ID");
        }
    }
}
