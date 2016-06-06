using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class UpdateRoles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "UserInformation_ID", c => c.Guid());
            CreateIndex("dbo.Roles", "UserInformation_ID");
            AddForeignKey("dbo.Roles", "UserInformation_ID", "dbo.UserInformations", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "UserInformation_ID", "dbo.UserInformations");
            DropIndex("dbo.Roles", new[] { "UserInformation_ID" });
            DropColumn("dbo.Roles", "UserInformation_ID");
        }
    }
}
