using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class PostViews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WallPosts", "Views", c => c.Int(nullable: false));
            DropColumn("dbo.WallPosts", "Deleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WallPosts", "Deleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.WallPosts", "Views");
        }
    }
}
