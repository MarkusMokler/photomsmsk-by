namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HallTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Halls", "HallType", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Halls", "HallType");
        }
    }
}
