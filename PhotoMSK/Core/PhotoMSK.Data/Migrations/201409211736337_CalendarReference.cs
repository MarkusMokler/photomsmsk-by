using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class CalendarReference : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalendarReferences",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CopyFrom = c.String(),
                        LastCollectTime = c.DateTime(nullable: false),
                        CalendarID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Calendars", t => t.CalendarID, cascadeDelete: true)
                .Index(t => t.CalendarID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CalendarReferences", "CalendarID", "dbo.Calendars");
            DropIndex("dbo.CalendarReferences", new[] { "CalendarID" });
            DropTable("dbo.CalendarReferences");
        }
    }
}
