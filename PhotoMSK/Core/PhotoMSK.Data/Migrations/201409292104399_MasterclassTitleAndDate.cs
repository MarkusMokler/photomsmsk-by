using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class MasterclassTitleAndDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Masterclass", "Title", c => c.String());
            AddColumn("dbo.Masterclass", "Start", c => c.DateTime(nullable: false));
            AddColumn("dbo.Masterclass", "End", c => c.DateTime(nullable: false));
            AddColumn("dbo.Masterclass", "Days", c => c.Int(nullable: false));
            AddColumn("dbo.Masterclass", "Listeners", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Masterclass", "Listeners");
            DropColumn("dbo.Masterclass", "Days");
            DropColumn("dbo.Masterclass", "End");
            DropColumn("dbo.Masterclass", "Start");
            DropColumn("dbo.Masterclass", "Title");
        }
    }
}
