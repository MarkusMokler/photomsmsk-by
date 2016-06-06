namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LegalInformation_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LegalInformations", "LegalName", c => c.String());
            AddColumn("dbo.LegalInformations", "CEO", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LegalInformations", "CEO");
            DropColumn("dbo.LegalInformations", "LegalName");
        }
    }
}
