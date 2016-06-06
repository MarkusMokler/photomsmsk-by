using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class Rents : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RentCalendars", new[] { "Photorent_ID" });
            DropIndex("dbo.RentCalendars", new[] { "Phototechnics_ID" });
            RenameColumn(table: "dbo.RentCalendars", name: "Photorent_ID", newName: "PhotorentID");
            RenameColumn(table: "dbo.RentCalendars", name: "Phototechnics_ID", newName: "PhototechnicsID");
            AlterColumn("dbo.RentCalendars", "PhotorentID", c => c.Guid(nullable: false));
            AlterColumn("dbo.RentCalendars", "PhototechnicsID", c => c.Guid(nullable: false));
            CreateIndex("dbo.RentCalendars", "PhotorentID");
            CreateIndex("dbo.RentCalendars", "PhototechnicsID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RentCalendars", new[] { "PhototechnicsID" });
            DropIndex("dbo.RentCalendars", new[] { "PhotorentID" });
            AlterColumn("dbo.RentCalendars", "PhototechnicsID", c => c.Guid());
            AlterColumn("dbo.RentCalendars", "PhotorentID", c => c.Guid());
            RenameColumn(table: "dbo.RentCalendars", name: "PhototechnicsID", newName: "Phototechnics_ID");
            RenameColumn(table: "dbo.RentCalendars", name: "PhotorentID", newName: "Photorent_ID");
            CreateIndex("dbo.RentCalendars", "Phototechnics_ID");
            CreateIndex("dbo.RentCalendars", "Photorent_ID");
        }
    }
}
