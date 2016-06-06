namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MonthAndbalance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BalanceLines",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Year = c.Int(nullable: false),
                        Title = c.String(),
                        Date = c.DateTime(),
                        Total = c.Double(nullable: false),
                        BalanceLineType = c.Int(nullable: false),
                        WeekOfMonth = c.Int(),
                        WeekOfYear = c.Int(),
                        RouteID = c.Guid(nullable: false),
                        WeekBalanceID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.RouteID)
                .ForeignKey("dbo.WeekBalances", t => t.WeekBalanceID)
                .Index(t => t.RouteID)
                .Index(t => t.WeekBalanceID);
            
            CreateTable(
                "dbo.WeekBalances",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RouteID = c.Guid(nullable: false),
                        StartBalance = c.Double(nullable: false),
                        EndBalance = c.Double(nullable: false),
                        Closed = c.Boolean(nullable: false),
                        WeekOfMonth = c.Int(nullable: false),
                        WeekOfYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.RouteID)
                .Index(t => t.RouteID);
            
            CreateTable(
                "dbo.Compares",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        NominationPhoto1ID = c.Guid(nullable: false),
                        NominationPhoto2ID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.NominationPhotoes", t => t.NominationPhoto1ID)
                .ForeignKey("dbo.NominationPhotoes", t => t.NominationPhoto2ID)
                .ForeignKey("dbo.UserInformations", t => t.UserID)
                .Index(t => t.NominationPhoto1ID)
                .Index(t => t.NominationPhoto2ID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.NominationPhotoes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CompareCount = c.Int(nullable: false),
                        Point = c.Int(nullable: false),
                        PhotoID = c.Guid(nullable: false),
                        MonthID = c.Guid(nullable: false),
                        PhotographerID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Months", t => t.MonthID)
                .ForeignKey("dbo.Photos", t => t.PhotoID)
                .ForeignKey("dbo.Photographer", t => t.PhotographerID)
                .Index(t => t.PhotoID)
                .Index(t => t.MonthID)
                .Index(t => t.PhotographerID);
            
            CreateTable(
                "dbo.Months",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DateMonth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Compares", "UserID", "dbo.UserInformations");
            DropForeignKey("dbo.Compares", "NominationPhoto2ID", "dbo.NominationPhotoes");
            DropForeignKey("dbo.Compares", "NominationPhoto1ID", "dbo.NominationPhotoes");
            DropForeignKey("dbo.NominationPhotoes", "PhotographerID", "dbo.Photographer");
            DropForeignKey("dbo.NominationPhotoes", "PhotoID", "dbo.Photos");
            DropForeignKey("dbo.NominationPhotoes", "MonthID", "dbo.Months");
            DropForeignKey("dbo.WeekBalances", "RouteID", "dbo.RouteEntity");
            DropForeignKey("dbo.BalanceLines", "WeekBalanceID", "dbo.WeekBalances");
            DropForeignKey("dbo.BalanceLines", "RouteID", "dbo.RouteEntity");
            DropIndex("dbo.NominationPhotoes", new[] { "PhotographerID" });
            DropIndex("dbo.NominationPhotoes", new[] { "MonthID" });
            DropIndex("dbo.NominationPhotoes", new[] { "PhotoID" });
            DropIndex("dbo.Compares", new[] { "UserID" });
            DropIndex("dbo.Compares", new[] { "NominationPhoto2ID" });
            DropIndex("dbo.Compares", new[] { "NominationPhoto1ID" });
            DropIndex("dbo.WeekBalances", new[] { "RouteID" });
            DropIndex("dbo.BalanceLines", new[] { "WeekBalanceID" });
            DropIndex("dbo.BalanceLines", new[] { "RouteID" });
            DropTable("dbo.Months");
            DropTable("dbo.NominationPhotoes");
            DropTable("dbo.Compares");
            DropTable("dbo.WeekBalances");
            DropTable("dbo.BalanceLines");
        }
    }
}
