namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LegalInformation_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LegalInformations", "ModifiedBy_ID", "dbo.UserInformations");
            DropIndex("dbo.LegalInformations", new[] { "ModifiedBy_ID" });
            RenameColumn(table: "dbo.LegalInformations", name: "ModifiedBy_ID", newName: "ModifiedByID");
            AlterColumn("dbo.LegalInformations", "ModifiedByID", c => c.Guid(nullable: false));
            CreateIndex("dbo.LegalInformations", "ModifiedByID");
            AddForeignKey("dbo.LegalInformations", "ModifiedByID", "dbo.UserInformations", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LegalInformations", "ModifiedByID", "dbo.UserInformations");
            DropIndex("dbo.LegalInformations", new[] { "ModifiedByID" });
            AlterColumn("dbo.LegalInformations", "ModifiedByID", c => c.Guid());
            RenameColumn(table: "dbo.LegalInformations", name: "ModifiedByID", newName: "ModifiedBy_ID");
            CreateIndex("dbo.LegalInformations", "ModifiedBy_ID");
            AddForeignKey("dbo.LegalInformations", "ModifiedBy_ID", "dbo.UserInformations", "ID");
        }
    }
}
