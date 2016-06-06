namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdersAndContracts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderLines", "PhototechnicsID", "dbo.Phototechnics");
            DropIndex("dbo.OrderLines", new[] { "PhototechnicsID" });
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        OrderID = c.Guid(nullable: false),
                        BarCode = c.Int(nullable: false),
                        Closed = c.Boolean(nullable: false),
                        Template_ID = c.Guid(nullable: false),
                        RouteEntity_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.ID)
                .ForeignKey("dbo.ContractTemplates", t => t.Template_ID, cascadeDelete: true)
                .ForeignKey("dbo.RouteEntity", t => t.RouteEntity_ID)
                .Index(t => t.ID)
                .Index(t => t.Template_ID)
                .Index(t => t.RouteEntity_ID);
            
            CreateTable(
                "dbo.ContractTemplates",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RouteID = c.Guid(nullable: false),
                        Template = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.RouteID, cascadeDelete: true)
                .Index(t => t.RouteID);
            
            AddColumn("dbo.RouteEntity", "LastDocumentCode", c => c.Int(nullable: false));
            AddColumn("dbo.Photoshops", "LastPositionCode", c => c.Int(nullable: false));
            AddColumn("dbo.Photorent", "LastCaldendarCode", c => c.Int(nullable: false));
            AddColumn("dbo.OrderLines", "EventID", c => c.Guid());
            AddColumn("dbo.OrderLines", "PricePositionID", c => c.Guid());
            AddColumn("dbo.OrderLines", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.PricePositions", "BarCode", c => c.Int(nullable: false));
            AddColumn("dbo.RentCalendars", "BarCode", c => c.Int(nullable: false));
            AddColumn("dbo.DeleteRouteRequests", "Reason", c => c.String());
            AlterColumn("dbo.OrderLines", "Count", c => c.Int());
            CreateIndex("dbo.OrderLines", "EventID");
            CreateIndex("dbo.OrderLines", "PricePositionID");
            AddForeignKey("dbo.OrderLines", "EventID", "dbo.Events", "ID", cascadeDelete: true);
            AddForeignKey("dbo.OrderLines", "PricePositionID", "dbo.PricePositions", "ID", cascadeDelete: true);
            DropColumn("dbo.OrderLines", "PhototechnicsID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderLines", "PhototechnicsID", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Contracts", "RouteEntity_ID", "dbo.RouteEntity");
            DropForeignKey("dbo.Contracts", "Template_ID", "dbo.ContractTemplates");
            DropForeignKey("dbo.ContractTemplates", "RouteID", "dbo.RouteEntity");
            DropForeignKey("dbo.Contracts", "ID", "dbo.Orders");
            DropForeignKey("dbo.OrderLines", "PricePositionID", "dbo.PricePositions");
            DropForeignKey("dbo.OrderLines", "EventID", "dbo.Events");
            DropIndex("dbo.ContractTemplates", new[] { "RouteID" });
            DropIndex("dbo.OrderLines", new[] { "PricePositionID" });
            DropIndex("dbo.OrderLines", new[] { "EventID" });
            DropIndex("dbo.Contracts", new[] { "RouteEntity_ID" });
            DropIndex("dbo.Contracts", new[] { "Template_ID" });
            DropIndex("dbo.Contracts", new[] { "ID" });
            AlterColumn("dbo.OrderLines", "Count", c => c.Int(nullable: false));
            DropColumn("dbo.DeleteRouteRequests", "Reason");
            DropColumn("dbo.RentCalendars", "BarCode");
            DropColumn("dbo.PricePositions", "BarCode");
            DropColumn("dbo.OrderLines", "Discriminator");
            DropColumn("dbo.OrderLines", "PricePositionID");
            DropColumn("dbo.OrderLines", "EventID");
            DropColumn("dbo.Photorent", "LastCaldendarCode");
            DropColumn("dbo.Photoshops", "LastPositionCode");
            DropColumn("dbo.RouteEntity", "LastDocumentCode");
            DropTable("dbo.ContractTemplates");
            DropTable("dbo.Contracts");
            CreateIndex("dbo.OrderLines", "PhototechnicsID");
            AddForeignKey("dbo.OrderLines", "PhototechnicsID", "dbo.Phototechnics", "ID");
        }
    }
}
