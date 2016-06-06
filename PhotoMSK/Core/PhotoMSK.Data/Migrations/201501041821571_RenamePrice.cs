using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class RenamePrice : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.RentCalendars", "Price", "DaylyPrice");
        }

        public override void Down()
        {

            RenameColumn("dbo.RentCalendars", "DaylyPrice", "Price");
        }
    }
}
