namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrackUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "CreatedByID", c => c.Guid());
            CreateIndex("dbo.Events", "CreatedByID");
            AddForeignKey("dbo.Events", "CreatedByID", "dbo.UserInformations", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "CreatedByID", "dbo.UserInformations");
            DropIndex("dbo.Events", new[] { "CreatedByID" });
            DropColumn("dbo.Events", "CreatedByID");
        }
    }
}
