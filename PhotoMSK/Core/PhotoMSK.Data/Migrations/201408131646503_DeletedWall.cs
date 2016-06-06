using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class DeletedWall : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WallPosts", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WallPosts", "Deleted");
        }
    }
}
