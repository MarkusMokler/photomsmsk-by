namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LegalInformation_4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LegalInformations", "CertificateNumber", c => c.String());
            DropColumn("dbo.LegalInformations", "СertificateNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LegalInformations", "СertificateNumber", c => c.String());
            DropColumn("dbo.LegalInformations", "CertificateNumber");
        }
    }
}
