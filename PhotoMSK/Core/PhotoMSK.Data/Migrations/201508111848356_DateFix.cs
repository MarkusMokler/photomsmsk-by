namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RouteEntity", "RegisterDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RouteEntity", "RegisterDate", c => c.DateTime(nullable: false));
        }
    }
}
