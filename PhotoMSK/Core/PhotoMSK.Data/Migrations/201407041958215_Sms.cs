using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class Sms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SmsMessages",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Phone = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SmsMessages");
        }
    }
}
