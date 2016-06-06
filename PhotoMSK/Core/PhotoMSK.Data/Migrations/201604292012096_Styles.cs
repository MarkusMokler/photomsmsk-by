namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Styles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThemeStyles",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RouteID = c.Guid(nullable: false),
                        Name = c.String(),
                        Style = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.RouteID, cascadeDelete: true)
                .Index(t => t.RouteID);
            
            AddColumn("dbo.RoutepageLayouts", "StyleID", c => c.Guid());
            CreateIndex("dbo.RoutepageLayouts", "StyleID");
            AddForeignKey("dbo.RoutepageLayouts", "StyleID", "dbo.ThemeStyles", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoutepageLayouts", "StyleID", "dbo.ThemeStyles");
            DropForeignKey("dbo.ThemeStyles", "RouteID", "dbo.RouteEntity");
            DropIndex("dbo.ThemeStyles", new[] { "RouteID" });
            DropIndex("dbo.RoutepageLayouts", new[] { "StyleID" });
            DropColumn("dbo.RoutepageLayouts", "StyleID");
            DropTable("dbo.ThemeStyles");
        }
    }
}
