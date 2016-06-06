namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmsAditional : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SmsModules", "BookingHelloText", c => c.String());
            AddColumn("dbo.SmsModules", "BookingEndText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SmsModules", "BookingEndText");
            DropColumn("dbo.SmsModules", "BookingHelloText");
        }
    }
}
