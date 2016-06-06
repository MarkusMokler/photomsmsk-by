namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2Widgets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalendarWidgets",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        WidgetID = c.Guid(nullable: false),
                        CalendarID = c.Guid(nullable: false),
                        Position = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Calendars", t => t.CalendarID)
                .ForeignKey("dbo.CalendarsWidget", t => t.WidgetID)
                .Index(t => t.WidgetID)
                .Index(t => t.CalendarID);
            
            CreateTable(
                "dbo.CalendarsWidget",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BaseWidget", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.HallWidget",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        HallID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BaseWidget", t => t.ID)
                .ForeignKey("dbo.Halls", t => t.HallID)
                .Index(t => t.ID)
                .Index(t => t.HallID);
            
            AddColumn("dbo.RoutepageLayouts", "Category", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HallWidget", "HallID", "dbo.Halls");
            DropForeignKey("dbo.HallWidget", "ID", "dbo.BaseWidget");
            DropForeignKey("dbo.CalendarsWidget", "ID", "dbo.BaseWidget");
            DropForeignKey("dbo.CalendarWidgets", "WidgetID", "dbo.CalendarsWidget");
            DropForeignKey("dbo.CalendarWidgets", "CalendarID", "dbo.Calendars");
            DropIndex("dbo.HallWidget", new[] { "HallID" });
            DropIndex("dbo.HallWidget", new[] { "ID" });
            DropIndex("dbo.CalendarsWidget", new[] { "ID" });
            DropIndex("dbo.CalendarWidgets", new[] { "CalendarID" });
            DropIndex("dbo.CalendarWidgets", new[] { "WidgetID" });
            DropColumn("dbo.RoutepageLayouts", "Category");
            DropTable("dbo.HallWidget");
            DropTable("dbo.CalendarsWidget");
            DropTable("dbo.CalendarWidgets");
        }
    }
}
