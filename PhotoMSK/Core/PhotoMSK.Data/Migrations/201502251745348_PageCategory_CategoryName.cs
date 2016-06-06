using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class PageCategory_CategoryName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PageCategories", "CategoryName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PageCategories", "CategoryName");
        }
    }
}
