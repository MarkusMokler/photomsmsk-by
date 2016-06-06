namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdPhotostudio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photostudio", "MinBookingTime", c => c.Int(nullable:false,defaultValue:60));
            AddColumn("dbo.Photostudio", "BookingTimeInc", c => c.Int(nullable:false,defaultValue:30));
        }
        
        public override void Down()
        {
        }
    }
}
