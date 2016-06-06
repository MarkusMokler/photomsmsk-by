namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Activity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activity",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        ActivityTime = c.DateTime(nullable: false),
                        RouteID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.RouteID)
                .ForeignKey("dbo.UserInformations", t => t.UserID)
                .Index(t => t.RouteID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.EventActivity",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        EventID = c.Guid(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Activity", t => t.ID)
                .ForeignKey("dbo.Events", t => t.EventID)
                .Index(t => t.ID)
                .Index(t => t.EventID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventActivity", "EventID", "dbo.Events");
            DropForeignKey("dbo.EventActivity", "ID", "dbo.Activity");
            DropForeignKey("dbo.Activity", "UserID", "dbo.UserInformations");
            DropForeignKey("dbo.Activity", "RouteID", "dbo.RouteEntity");
            DropIndex("dbo.EventActivity", new[] { "EventID" });
            DropIndex("dbo.EventActivity", new[] { "ID" });
            DropIndex("dbo.Activity", new[] { "UserID" });
            DropIndex("dbo.Activity", new[] { "RouteID" });
            DropTable("dbo.EventActivity");
            DropTable("dbo.Activity");
        }
    }
}
