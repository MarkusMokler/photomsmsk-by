namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PageType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasePages", "PageType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BasePages", "PageType");
        }
    }
}
