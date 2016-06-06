namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmsAndHalls : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SmsModules",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SmsEnnabled = c.Boolean(nullable: false),
                        BookingLineText = c.String(),
                        SmsBalance = c.Double(nullable: false),
                        SmsSender = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.ID)
                .Index(t => t.ID);
            
            AddColumn("dbo.Halls", "Position", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SmsModules", "ID", "dbo.RouteEntity");
            DropIndex("dbo.SmsModules", new[] { "ID" });
            DropColumn("dbo.Halls", "Position");
            DropTable("dbo.SmsModules");
        }
    }
}
