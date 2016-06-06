namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LegalInformation_1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RouteEntity", "LegalInformationID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RouteEntity", "LegalInformationID", c => c.Guid());
        }
    }
}
