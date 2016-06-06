namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CardsUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SaleCards", "CardType", c => c.String());
            AddColumn("dbo.SaleCards", "FirstName", c => c.String());
            AddColumn("dbo.SaleCards", "LastName", c => c.String());
            AddColumn("dbo.SaleCards", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.SaleCards", "PasportNumber", c => c.String());
            AddColumn("dbo.SaleCards", "PersonalNumber", c => c.String());
            AddColumn("dbo.SaleCards", "DateOfIssue", c => c.DateTime(nullable: false));
            AddColumn("dbo.SaleCards", "DepartmentOfIssue", c => c.String());
            AddColumn("dbo.SaleCards", "Country", c => c.String());
            AddColumn("dbo.SaleCards", "City", c => c.String());
            AddColumn("dbo.SaleCards", "Address", c => c.String());
            AddColumn("dbo.SaleCards", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SaleCards", "Photo");
            DropColumn("dbo.SaleCards", "Address");
            DropColumn("dbo.SaleCards", "City");
            DropColumn("dbo.SaleCards", "Country");
            DropColumn("dbo.SaleCards", "DepartmentOfIssue");
            DropColumn("dbo.SaleCards", "DateOfIssue");
            DropColumn("dbo.SaleCards", "PersonalNumber");
            DropColumn("dbo.SaleCards", "PasportNumber");
            DropColumn("dbo.SaleCards", "DateOfBirth");
            DropColumn("dbo.SaleCards", "LastName");
            DropColumn("dbo.SaleCards", "FirstName");
            DropColumn("dbo.SaleCards", "CardType");
        }
    }
}
