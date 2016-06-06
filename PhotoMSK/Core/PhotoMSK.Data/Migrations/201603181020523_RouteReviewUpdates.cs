namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RouteReviewUpdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RouteReviews", "Date", c => c.DateTime(nullable: false, defaultValue: DateTime.Now));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RouteReviews", "Date");
        }
    }
}
