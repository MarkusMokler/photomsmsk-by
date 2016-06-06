namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BalansYear : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WeekBalances", "Year", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WeekBalances", "Year");
        }
    }
}
