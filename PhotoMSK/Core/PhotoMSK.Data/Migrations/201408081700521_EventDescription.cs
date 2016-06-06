using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class EventDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Description");
        }
    }
}
