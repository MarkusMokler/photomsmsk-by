using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class ShootingGenre : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Shooting", "Gener_ID", "dbo.PhotoGeners");
            DropIndex("dbo.Shooting", new[] { "Gener_ID" });
            CreateTable(
                "dbo.ShootingGenres",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        GenreID = c.Guid(nullable: false),
                        ShootingID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Genres", t => t.GenreID)
                .ForeignKey("dbo.Shooting", t => t.ShootingID)
                .Index(t => t.GenreID)
                .Index(t => t.ShootingID);
            
            DropColumn("dbo.Shooting", "Gener_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shooting", "Gener_ID", c => c.Guid());
            DropForeignKey("dbo.ShootingGenres", "ShootingID", "dbo.Shooting");
            DropForeignKey("dbo.ShootingGenres", "GenreID", "dbo.Genres");
            DropIndex("dbo.ShootingGenres", new[] { "ShootingID" });
            DropIndex("dbo.ShootingGenres", new[] { "GenreID" });
            DropTable("dbo.ShootingGenres");
            CreateIndex("dbo.Shooting", "Gener_ID");
            AddForeignKey("dbo.Shooting", "Gener_ID", "dbo.PhotoGeners", "ID");
        }
    }
}
