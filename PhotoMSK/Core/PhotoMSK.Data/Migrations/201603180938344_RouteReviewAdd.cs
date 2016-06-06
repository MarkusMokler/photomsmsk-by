namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RouteReviewAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RouteReviews",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        GoodComment = c.String(),
                        BadComment = c.String(),
                        LikeStatus_ID = c.Guid(),
                        Route_ID = c.Guid(),
                        User_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LikeStatus", t => t.LikeStatus_ID)
                .ForeignKey("dbo.RouteEntity", t => t.Route_ID)
                .ForeignKey("dbo.UserInformations", t => t.User_ID)
                .Index(t => t.LikeStatus_ID)
                .Index(t => t.Route_ID)
                .Index(t => t.User_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RouteReviews", "User_ID", "dbo.UserInformations");
            DropForeignKey("dbo.RouteReviews", "Route_ID", "dbo.RouteEntity");
            DropForeignKey("dbo.RouteReviews", "LikeStatus_ID", "dbo.LikeStatus");
            DropIndex("dbo.RouteReviews", new[] { "User_ID" });
            DropIndex("dbo.RouteReviews", new[] { "Route_ID" });
            DropIndex("dbo.RouteReviews", new[] { "LikeStatus_ID" });
            DropTable("dbo.RouteReviews");
        }
    }
}
