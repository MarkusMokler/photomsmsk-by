namespace PhotoMSK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoppingCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShippingAddresses",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Street1 = c.String(),
                        Street2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        UserInformationID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserInformations", t => t.UserInformationID, cascadeDelete: true)
                .Index(t => t.UserInformationID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        StatusID = c.Guid(nullable: false),
                        UserInformationID = c.Guid(nullable: false),
                        ShippingAdressID = c.Guid(),
                        OrderDate = c.DateTime(nullable: false),
                        Total = c.Double(nullable: false),
                        ShippingCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Status", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("dbo.UserInformations", t => t.UserInformationID)
                .ForeignKey("dbo.ShippingAddresses", t => t.ShippingAdressID)
                .Index(t => t.StatusID)
                .Index(t => t.UserInformationID)
                .Index(t => t.ShippingAdressID);
            
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PhototechnicsID = c.Guid(nullable: false),
                        OrderID = c.Guid(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.OrderID)
                .ForeignKey("dbo.Phototechnics", t => t.PhototechnicsID)
                .Index(t => t.PhototechnicsID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShippingAddresses", "UserInformationID", "dbo.UserInformations");
            DropForeignKey("dbo.Orders", "ShippingAdressID", "dbo.ShippingAddresses");
            DropForeignKey("dbo.Orders", "UserInformationID", "dbo.UserInformations");
            DropForeignKey("dbo.Orders", "StatusID", "dbo.Status");
            DropForeignKey("dbo.OrderLines", "PhototechnicsID", "dbo.Phototechnics");
            DropForeignKey("dbo.OrderLines", "OrderID", "dbo.Orders");
            DropIndex("dbo.OrderLines", new[] { "OrderID" });
            DropIndex("dbo.OrderLines", new[] { "PhototechnicsID" });
            DropIndex("dbo.Orders", new[] { "ShippingAdressID" });
            DropIndex("dbo.Orders", new[] { "UserInformationID" });
            DropIndex("dbo.Orders", new[] { "StatusID" });
            DropIndex("dbo.ShippingAddresses", new[] { "UserInformationID" });
            DropTable("dbo.Status");
            DropTable("dbo.OrderLines");
            DropTable("dbo.Orders");
            DropTable("dbo.ShippingAddresses");
        }
    }
}
