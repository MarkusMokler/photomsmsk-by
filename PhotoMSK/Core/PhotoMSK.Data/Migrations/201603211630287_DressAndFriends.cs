namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DressAndFriends : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dress",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DressRentID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DressRent", t => t.DressRentID)
                .Index(t => t.DressRentID);
            
            CreateTable(
                "dbo.Friendships",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        BaseRouteID = c.Guid(nullable: false),
                        ChildRouteID = c.Guid(nullable: false),
                        FriendshipType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.BaseRouteID)
                .ForeignKey("dbo.RouteEntity", t => t.ChildRouteID)
                .Index(t => t.BaseRouteID)
                .Index(t => t.ChildRouteID);
            
            CreateTable(
                "dbo.DressRent",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.DressCalendar",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Dress_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Calendars", t => t.ID)
                .ForeignKey("dbo.Dress", t => t.Dress_ID)
                .Index(t => t.ID)
                .Index(t => t.Dress_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DressCalendar", "Dress_ID", "dbo.Dress");
            DropForeignKey("dbo.DressCalendar", "ID", "dbo.Calendars");
            DropForeignKey("dbo.DressRent", "ID", "dbo.RouteEntity");
            DropForeignKey("dbo.Friendships", "ChildRouteID", "dbo.RouteEntity");
            DropForeignKey("dbo.Friendships", "BaseRouteID", "dbo.RouteEntity");
            DropForeignKey("dbo.Dress", "DressRentID", "dbo.DressRent");
            DropIndex("dbo.DressCalendar", new[] { "Dress_ID" });
            DropIndex("dbo.DressCalendar", new[] { "ID" });
            DropIndex("dbo.DressRent", new[] { "ID" });
            DropIndex("dbo.Friendships", new[] { "ChildRouteID" });
            DropIndex("dbo.Friendships", new[] { "BaseRouteID" });
            DropIndex("dbo.Dress", new[] { "DressRentID" });
            DropTable("dbo.DressCalendar");
            DropTable("dbo.DressRent");
            DropTable("dbo.Friendships");
            DropTable("dbo.Dress");
        }
    }
}
