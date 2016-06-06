using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class UserInformationCreationTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInformations", "CreationTime", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
        }

        public override void Down()
        {
            DropColumn("dbo.UserInformations", "CreationTime");
        }
    }
}
