namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserInformationCupAndLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInformations", "IsCup", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserInformations", "Agreement", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserInformations", "VkLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInformations", "VkLink");
            DropColumn("dbo.UserInformations", "Agreement");
            DropColumn("dbo.UserInformations", "IsCup");
        }
    }
}
