namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileEntryName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileEntry", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileEntry", "Name");
        }
    }
}
