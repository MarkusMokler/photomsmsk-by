using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class Cards : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SaleCards",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserInformationID = c.Guid(nullable: false),
                        CardNumber = c.String(),
                        Pin = c.String(),
                        ExpiredDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserInformations", t => t.UserInformationID, cascadeDelete: true)
                .Index(t => t.UserInformationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleCards", "UserInformationID", "dbo.UserInformations");
            DropIndex("dbo.SaleCards", new[] { "UserInformationID" });
            DropTable("dbo.SaleCards");
        }
    }
}
