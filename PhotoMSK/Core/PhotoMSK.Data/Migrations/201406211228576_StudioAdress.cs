using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class StudioAdress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photostudio", "Adress", c => c.String());
            AddColumn("dbo.Photostudio", "Site", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photostudio", "Site");
            DropColumn("dbo.Photostudio", "Adress");
        }
    }
}
