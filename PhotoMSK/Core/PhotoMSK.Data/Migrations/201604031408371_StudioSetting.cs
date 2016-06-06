using PhotoMSK.Data.Models;

namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class StudioSetting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RouteSettings",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    SettingID = c.Guid(nullable: false),
                    RouteID = c.Guid(nullable: false),
                    SettingValue = c.String(),
                    Ennabled = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.RouteID)
                .ForeignKey("dbo.Settings", t => t.SettingID)
                .Index(t => t.SettingID)
                .Index(t => t.RouteID);

            CreateTable(
                "dbo.Settings",
                c => new
                {
                    ID = c.Guid(nullable: false),
                    SettingName = c.String(),
                    Description = c.String(),
                    Type = c.String(),
                })
                .PrimaryKey(t => t.ID);

            Sql("INSERT INTO Settings VALUES ('" + Setting.NOTIFY_ABOUT_BOOKING + "','Оповещение о бронировании','За какое время при бронировании высылаются смс-ки','int')");
            Sql("INSERT INTO Settings VALUES ('" + Setting.NOTIFY_NUMBER + "','Номер для оповещений','Номер на который высылаются смс-ки','string')");
        }

        public override void Down()
        {
            DropForeignKey("dbo.RouteSettings", "SettingID", "dbo.Settings");
            DropForeignKey("dbo.RouteSettings", "RouteID", "dbo.RouteEntity");
            DropIndex("dbo.RouteSettings", new[] { "RouteID" });
            DropIndex("dbo.RouteSettings", new[] { "SettingID" });
            DropTable("dbo.Settings");
            DropTable("dbo.RouteSettings");
        }



    }
}
