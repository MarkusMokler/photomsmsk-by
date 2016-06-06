namespace PhotoMSK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CurrencyInPhotorent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photorent", "CurrencyValue", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photorent", "CurrencyValue");
        }
    }
}
