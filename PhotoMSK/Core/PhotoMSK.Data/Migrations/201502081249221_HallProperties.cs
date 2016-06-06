using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class HallProperties : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HallProperties",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Group = c.Int(nullable: false),
                        Index = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HallPropertyHalls",
                c => new
                    {
                        HallProperty_ID = c.Guid(nullable: false),
                        Hall_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.HallProperty_ID, t.Hall_ID })
                .ForeignKey("dbo.HallProperties", t => t.HallProperty_ID, cascadeDelete: false)
                .ForeignKey("dbo.Halls", t => t.Hall_ID, cascadeDelete: false)
                .Index(t => t.HallProperty_ID)
                .Index(t => t.Hall_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HallPropertyHalls", "Hall_ID", "dbo.Halls");
            DropForeignKey("dbo.HallPropertyHalls", "HallProperty_ID", "dbo.HallProperties");
            DropIndex("dbo.HallPropertyHalls", new[] { "Hall_ID" });
            DropIndex("dbo.HallPropertyHalls", new[] { "HallProperty_ID" });
            DropTable("dbo.HallPropertyHalls");
            DropTable("dbo.HallProperties");
        }
    }
}
