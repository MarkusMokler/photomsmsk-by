namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueNumber1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Phones", new[] { "Number" });
            AlterColumn("dbo.Phones", "Number", c => c.String(maxLength: 50));
            CreateIndex("dbo.Phones", "Number", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Phones", new[] { "Number" });
            AlterColumn("dbo.Phones", "Number", c => c.String(maxLength: 255));
            CreateIndex("dbo.Phones", "Number", unique: true);
        }
    }
}
