using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class Values : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParameterValues", "Value", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParameterValues", "Value");
        }
    }
}
