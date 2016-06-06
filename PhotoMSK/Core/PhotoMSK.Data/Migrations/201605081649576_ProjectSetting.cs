namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectSetting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectSettings", "Discount", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectSettings", "Seats", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectSettings", "Time", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectSettings", "StartingDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProjectSettings", "Image", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProjectSettings", "ImageQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.ProjectSettings", "PhotoQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectSettings", "PhotoQuantity");
            DropColumn("dbo.ProjectSettings", "ImageQuantity");
            DropColumn("dbo.ProjectSettings", "Image");
            DropColumn("dbo.ProjectSettings", "StartingDate");
            DropColumn("dbo.ProjectSettings", "Time");
            DropColumn("dbo.ProjectSettings", "Seats");
            DropColumn("dbo.ProjectSettings", "Discount");
        }
    }
}
