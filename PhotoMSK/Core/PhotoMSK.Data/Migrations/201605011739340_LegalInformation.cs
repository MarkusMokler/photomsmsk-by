namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LegalInformation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LegalInformations",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        AllowOnlineBooking = c.Boolean(nullable: false),
                        PublicOffer = c.String(),
                        Legaladdress = c.String(),
                        AccountNumber = c.String(),
                        RegisterTrade = c.String(),
                        СertificateNumber = c.String(),
                        RegisterDate = c.DateTime(),
                        RegisteringAgency = c.String(),
                        Version = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        ModifiedTime = c.DateTime(nullable: false),
                        ModifiedBy_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserInformations", t => t.ModifiedBy_ID)
                .ForeignKey("dbo.RouteEntity", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.ModifiedBy_ID);
            
            AddColumn("dbo.RouteEntity", "LegalInformationID", c => c.Guid());
            DropColumn("dbo.RouteEntity", "AllowOnlineBooking");
            DropColumn("dbo.RouteEntity", "Legaladdress");
            DropColumn("dbo.RouteEntity", "AccountNumber");
            DropColumn("dbo.RouteEntity", "RegisterTrade");
            DropColumn("dbo.RouteEntity", "СertificateNumber");
            DropColumn("dbo.RouteEntity", "RegisterDate");
            DropColumn("dbo.RouteEntity", "RegisteringAgency");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RouteEntity", "RegisteringAgency", c => c.String());
            AddColumn("dbo.RouteEntity", "RegisterDate", c => c.DateTime());
            AddColumn("dbo.RouteEntity", "СertificateNumber", c => c.String());
            AddColumn("dbo.RouteEntity", "RegisterTrade", c => c.String());
            AddColumn("dbo.RouteEntity", "AccountNumber", c => c.String());
            AddColumn("dbo.RouteEntity", "Legaladdress", c => c.String());
            AddColumn("dbo.RouteEntity", "AllowOnlineBooking", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.LegalInformations", "ID", "dbo.RouteEntity");
            DropForeignKey("dbo.LegalInformations", "ModifiedBy_ID", "dbo.UserInformations");
            DropIndex("dbo.LegalInformations", new[] { "ModifiedBy_ID" });
            DropIndex("dbo.LegalInformations", new[] { "ID" });
            DropColumn("dbo.RouteEntity", "LegalInformationID");
            DropTable("dbo.LegalInformations");
        }
    }
}
