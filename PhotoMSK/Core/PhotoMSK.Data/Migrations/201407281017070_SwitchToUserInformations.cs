using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class SwitchToUserInformations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Penalties", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "User_Id" });
            DropIndex("dbo.Penalties", new[] { "User_Id" });
            DropColumn("dbo.Events", "User_Id");
            DropColumn("dbo.Penalties", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Penalties", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Events", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Penalties", "User_Id");
            CreateIndex("dbo.Events", "User_Id");
            AddForeignKey("dbo.Penalties", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Events", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
