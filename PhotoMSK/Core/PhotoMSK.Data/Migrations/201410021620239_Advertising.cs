using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class Advertising : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdCompanies",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Ads", "AdCompany_ID", c => c.Guid());
            CreateIndex("dbo.Ads", "AdCompany_ID");
            AddForeignKey("dbo.Ads", "AdCompany_ID", "dbo.AdCompanies", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ads", "AdCompany_ID", "dbo.AdCompanies");
            DropIndex("dbo.Ads", new[] { "AdCompany_ID" });
            DropColumn("dbo.Ads", "AdCompany_ID");
            DropTable("dbo.AdCompanies");
        }
    }
}
