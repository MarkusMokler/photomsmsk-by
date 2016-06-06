namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotostudioDaysOfClaim : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photostudio", "DaysOfClaim", c => c.Int(nullable: false, defaultValue: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photostudio", "DaysOfClaim");
        }
    }
}
