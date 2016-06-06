namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasePage : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TextPages", newName: "BasePages");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.BasePages", newName: "TextPages");
        }
    }
}
