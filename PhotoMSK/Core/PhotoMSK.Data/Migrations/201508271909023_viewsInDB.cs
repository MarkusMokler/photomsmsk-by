namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class viewsInDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DbRazorViews",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ThemeName = c.String(),
                        ThemeType = c.String(),
                        ViewName = c.String(),
                        RazorContent = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DbRazorViews");
        }
    }
}
