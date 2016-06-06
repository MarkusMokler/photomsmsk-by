namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventPrePayed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "PrePayed", c => c.Double(nullable: false, defaultValue:0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "PrePayed");
        }
    }
}
