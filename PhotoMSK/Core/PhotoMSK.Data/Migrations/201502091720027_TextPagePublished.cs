using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class TextPagePublished : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TextPages", "Published", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TextPages", "Published");
        }
    }
}
