namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GoogleID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "GoogleID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "GoogleID");
        }
    }
}
