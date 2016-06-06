using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class RefreshToken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoogleSheetsSyncs", "RefreshToken", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoogleSheetsSyncs", "RefreshToken");
        }
    }
}
