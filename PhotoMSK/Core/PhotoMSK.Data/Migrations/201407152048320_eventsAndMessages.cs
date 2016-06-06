using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class eventsAndMessages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adresses",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Readed = c.Boolean(nullable: false),
                        FromID = c.String(nullable: false, maxLength: 128),
                        ToID = c.String(nullable: false, maxLength: 128),
                        Message_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.FromID, cascadeDelete: false)
                .ForeignKey("dbo.Messages", t => t.Message_ID, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.ToID, cascadeDelete: false)
                .Index(t => t.FromID)
                .Index(t => t.ToID)
                .Index(t => t.Message_ID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MasterclassEvents",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Time = c.DateTime(nullable: false),
                        EventType = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Masterclass_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Masterclass", t => t.Masterclass_ID)
                .Index(t => t.Masterclass_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MasterclassEvents", "Masterclass_ID", "dbo.Masterclass");
            DropForeignKey("dbo.Adresses", "ToID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Adresses", "Message_ID", "dbo.Messages");
            DropForeignKey("dbo.Adresses", "FromID", "dbo.AspNetUsers");
            DropIndex("dbo.MasterclassEvents", new[] { "Masterclass_ID" });
            DropIndex("dbo.Adresses", new[] { "Message_ID" });
            DropIndex("dbo.Adresses", new[] { "ToID" });
            DropIndex("dbo.Adresses", new[] { "FromID" });
            DropTable("dbo.MasterclassEvents");
            DropTable("dbo.Messages");
            DropTable("dbo.Adresses");
        }
    }
}
