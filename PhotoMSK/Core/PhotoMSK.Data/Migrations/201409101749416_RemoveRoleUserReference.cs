using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class RemoveRoleUserReference : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Roles", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Roles", new[] { "User_Id" });
            DropColumn("dbo.Roles", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Roles", "User_Id");
            AddForeignKey("dbo.Roles", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
