using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class Prices : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RentCalendars", "HourlyPrice", c => c.Double(nullable: false));
            AddColumn("dbo.RentCalendars", "HalfDayPrice", c => c.Double(nullable: false));
            AddColumn("dbo.RentCalendars", "WeeklyPrice", c => c.Double(nullable: false));
            AddColumn("dbo.RentCalendars", "MonthlyPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RentCalendars", "MonthlyPrice");
            DropColumn("dbo.RentCalendars", "WeeklyPrice");
            DropColumn("dbo.RentCalendars", "HalfDayPrice");
            DropColumn("dbo.RentCalendars", "HourlyPrice");
        }
    }
}
