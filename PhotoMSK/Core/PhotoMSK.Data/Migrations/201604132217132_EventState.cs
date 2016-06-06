namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EventState", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "EventState");
        }
    }
}
