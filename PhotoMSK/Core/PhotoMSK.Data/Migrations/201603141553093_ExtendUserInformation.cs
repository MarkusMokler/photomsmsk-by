namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendUserInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInformations", "ClientType", c => c.String());
            AddColumn("dbo.UserInformations", "Email", c => c.String());
            AddColumn("dbo.UserInformations", "City", c => c.String());
            AddColumn("dbo.UserInformations", "Country", c => c.String());
            AddColumn("dbo.UserInformations", "DateOfBirth", c => c.DateTime());
            AddColumn("dbo.UserInformations", "FacebookLink", c => c.String());
            AddColumn("dbo.UserInformations", "Instagram", c => c.String());
            AddColumn("dbo.UserInformations", "Googleplus", c => c.String());
            AddColumn("dbo.UserInformations", "Site", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInformations", "Site");
            DropColumn("dbo.UserInformations", "Googleplus");
            DropColumn("dbo.UserInformations", "Instagram");
            DropColumn("dbo.UserInformations", "FacebookLink");
            DropColumn("dbo.UserInformations", "DateOfBirth");
            DropColumn("dbo.UserInformations", "Country");
            DropColumn("dbo.UserInformations", "City");
            DropColumn("dbo.UserInformations", "Email");
            DropColumn("dbo.UserInformations", "ClientType");
        }
    }
}
