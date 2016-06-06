namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CallActivity : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CallActivity", name: "Event_ID", newName: "EventID");
            RenameIndex(table: "dbo.CallActivity", name: "IX_Event_ID", newName: "IX_EventID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CallActivity", name: "IX_EventID", newName: "IX_Event_ID");
            RenameColumn(table: "dbo.CallActivity", name: "EventID", newName: "Event_ID");
        }
    }
}
