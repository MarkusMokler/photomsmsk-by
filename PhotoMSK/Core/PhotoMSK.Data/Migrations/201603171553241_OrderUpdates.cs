namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class OrderUpdates : DbMigration
    {
        public override void Up()
        {
            //      AddColumn("dbo.Photostudio", "MinBookingTime", c => c.Int(nullable: false));
            //    AddColumn("dbo.Photostudio", "BookingTimeInc", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Photostudio", "BookingTimeInc");
            DropColumn("dbo.Photostudio", "MinBookingTime");
        }
    }
}
