using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class GoogleSync : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GoogleSheetsSyncs",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Token = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GoogleSheetsSyncs");
        }
    }
}
