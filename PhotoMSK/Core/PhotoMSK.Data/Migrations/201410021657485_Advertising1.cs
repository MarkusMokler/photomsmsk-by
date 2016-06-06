using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class Advertising1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Ads", name: "AdCompany_ID", newName: "Company_ID");
            RenameIndex(table: "dbo.Ads", name: "IX_AdCompany_ID", newName: "IX_Company_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Ads", name: "IX_Company_ID", newName: "IX_AdCompany_ID");
            RenameColumn(table: "dbo.Ads", name: "Company_ID", newName: "AdCompany_ID");
        }
    }
}
