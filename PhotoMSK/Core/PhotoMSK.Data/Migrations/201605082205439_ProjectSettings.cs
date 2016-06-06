namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInformations", "ProjectSettings_ID", c => c.Guid());
            AddColumn("dbo.ProjectSettings", "Location", c => c.String());
            CreateIndex("dbo.UserInformations", "ProjectSettings_ID");
            AddForeignKey("dbo.UserInformations", "ProjectSettings_ID", "dbo.ProjectSettings", "ID");
            DropColumn("dbo.ProjectSettings", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProjectSettings", "Value", c => c.String());
            DropForeignKey("dbo.UserInformations", "ProjectSettings_ID", "dbo.ProjectSettings");
            DropIndex("dbo.UserInformations", new[] { "ProjectSettings_ID" });
            DropColumn("dbo.ProjectSettings", "Location");
            DropColumn("dbo.UserInformations", "ProjectSettings_ID");
        }
    }
}
