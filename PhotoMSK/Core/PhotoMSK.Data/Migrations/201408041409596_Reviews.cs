using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class Reviews : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            AlterColumn("dbo.Reviews", "User_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Reviews", "User_Id");
            AddForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            AlterColumn("dbo.Reviews", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reviews", "User_Id");
            AddForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
