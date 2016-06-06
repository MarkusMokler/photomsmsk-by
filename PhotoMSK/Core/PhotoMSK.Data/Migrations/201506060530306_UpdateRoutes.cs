namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateRoutes2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeleteRouteRequests",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        RouteID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.RouteID, cascadeDelete: true)
                .Index(t => t.RouteID);

            AddColumn("dbo.Photomodel", "MartialStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Photomodel", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.Photomodel", "Email", c => c.String());
            AddColumn("dbo.RouteEntity", "Country", c => c.String());
            AddColumn("dbo.RouteEntity", "City", c => c.String());
            AddColumn("dbo.RouteEntity", "Latitude", c => c.Double());
            AddColumn("dbo.RouteEntity", "Longitude", c => c.Double());
            AddColumn("dbo.RouteEntity", "Address", c => c.String());
            AddColumn("dbo.RouteEntity", "MetroStation", c => c.String());
            AddColumn("dbo.RouteEntity", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.RouteEntity", "Legaladdress", c => c.String());
            AddColumn("dbo.RouteEntity", "AccountNumber", c => c.String());
            AddColumn("dbo.RouteEntity", "RegisterTrade", c => c.String());
            AddColumn("dbo.RouteEntity", "СertificateNumber", c => c.String());
            AddColumn("dbo.RouteEntity", "RegisterDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RouteEntity", "RegisteringAgency", c => c.String());
            AddColumn("dbo.Masterclass", "AuthorLastName", c => c.String());
            AddColumn("dbo.Masterclass", "VisitingCity", c => c.String());
            AddColumn("dbo.Photostudio", "HallCount", c => c.Int(nullable: false,defaultValue:0));

            DropColumn("dbo.Photomodel", "EyesColor");
            DropColumn("dbo.Photomodel", "FaceType");

            AddColumn("dbo.Photomodel", "EyesColor", c => c.Int(nullable: true));
            AddColumn("dbo.Photomodel", "FaceType", c => c.Int(nullable: true));

            string str = "Update dbo.[RouteEntity] " +
             "set City=CHILD.City, MetroStation=CHILD.MetroStation " +
             "from dbo.[RouteEntity] RE INNER JOIN {0} CHILD ON RE.ID = CHILD.ID";


            Sql(String.Format(str, "dbo.Photoshops"));

            DropColumn("dbo.Photoshops", "City");
            DropColumn("dbo.Photoshops", "MetroStation");


            Sql(String.Format(str, "dbo.Photorent"));
            DropColumn("dbo.Photorent", "City");
            DropColumn("dbo.Photorent", "MetroStation");



            Sql(String.Format(str, "dbo.Photostudio"));

            DropColumn("dbo.Photostudio", "City");
            DropColumn("dbo.Photostudio", "MetroStation");



            str = "Update dbo.[RouteEntity]" +
             "set City=CHILD.City " +
             "from dbo.[RouteEntity] RE INNER JOIN {0} CHILD ON RE.ID = CHILD.ID";

            Sql(String.Format(str, "dbo.Photomodel"));

            DropColumn("dbo.Photomodel", "City");

            Sql(String.Format(str, "dbo.Photographer"));

            DropColumn("dbo.Photographer", "City");

            str = "Update dbo.[RouteEntity]" +
             "set Latitude=CHILD.Latitude, Longitude=CHILD.Longitude " +
             "from dbo.[RouteEntity] RE INNER JOIN {0} CHILD ON RE.ID = CHILD.ID";


            Sql(String.Format(str, "dbo.Photostudio"));



            Sql(String.Format(str, "dbo.Places"));


            DropColumn("dbo.Photostudio", "Latitude");
            DropColumn("dbo.Photostudio", "Longitude");

            DropColumn("dbo.Places", "Longitude");
            DropColumn("dbo.Places", "Latitude");


            DropColumn("dbo.Photomodel", "Age");

            }

        public override void Down()
        {
            AddColumn("dbo.Places", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Places", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.Photorent", "MetroStation", c => c.String());
            AddColumn("dbo.Photorent", "City", c => c.String());
            AddColumn("dbo.Photostudio", "MetroStation", c => c.String());
            AddColumn("dbo.Photostudio", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.Photostudio", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Photostudio", "City", c => c.String());
            AddColumn("dbo.Photographer", "City", c => c.String());
            AddColumn("dbo.Photomodel", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Photomodel", "City", c => c.String());
            AddColumn("dbo.Photoshops", "MetroStation", c => c.String());
            AddColumn("dbo.Photoshops", "City", c => c.String());
            DropForeignKey("dbo.DeleteRouteRequests", "RouteID", "dbo.RouteEntity");
            DropIndex("dbo.DeleteRouteRequests", new[] { "RouteID" });
            AlterColumn("dbo.Photomodel", "FaceType", c => c.String());
            AlterColumn("dbo.Photomodel", "EyesColor", c => c.String());
            DropColumn("dbo.Photostudio", "HallCount");
            DropColumn("dbo.Masterclass", "VisitingCity");
            DropColumn("dbo.Masterclass", "AuthorLastName");
            DropColumn("dbo.RouteEntity", "RegisteringAgency");
            DropColumn("dbo.RouteEntity", "RegisterDate");
            DropColumn("dbo.RouteEntity", "СertificateNumber");
            DropColumn("dbo.RouteEntity", "RegisterTrade");
            DropColumn("dbo.RouteEntity", "AccountNumber");
            DropColumn("dbo.RouteEntity", "Legaladdress");
            DropColumn("dbo.RouteEntity", "Active");
            DropColumn("dbo.RouteEntity", "MetroStation");
            DropColumn("dbo.RouteEntity", "Address");
            DropColumn("dbo.RouteEntity", "Longitude");
            DropColumn("dbo.RouteEntity", "Latitude");
            DropColumn("dbo.RouteEntity", "City");
            DropColumn("dbo.RouteEntity", "Country");
            DropColumn("dbo.Photomodel", "Email");
            DropColumn("dbo.Photomodel", "DateOfBirth");
            DropColumn("dbo.Photomodel", "MartialStatus");
            DropTable("dbo.DeleteRouteRequests");
        }
    }
}
