using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class PhotoshopUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Phototechnics", "Manufacture_ID", "dbo.Brands");
            DropForeignKey("dbo.Phototechnics", "TechnicsType_ID", "dbo.Categories");
           
            DropIndex("dbo.Phototechnics", new[] { "Manufacture_ID" });
            DropIndex("dbo.Phototechnics", new[] { "TechnicsType_ID" });
            
            RenameTable("dbo.Brands", "Brands");
            RenameTable("dbo.Categories", "Categories");
            

            AddColumn("dbo.Categories", "ImageUrl", x => x.String());

            CreateTable(
                "dbo.CategoryBrand",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CategoryID = c.Guid(nullable: false),
                        BrandID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Brands", t => t.BrandID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .Index(t => t.CategoryID)
                .Index(t => t.BrandID);

            CreateTable(
                   "dbo.ShopCategory",
                   c => new
                       {
                           ID = c.Guid(nullable: false),
                           PhotoshopID = c.Guid(nullable: false),
                           Order = c.Int(nullable: false),
                       })
                   .PrimaryKey(t => t.ID)
                   .ForeignKey("dbo.Categories", t => t.ID)
                   .ForeignKey("dbo.Photoshops", t => t.PhotoshopID)
                   .Index(t => t.ID)
                   .Index(t => t.PhotoshopID);

            RenameColumn("dbo.Phototechnics", "Manufacture_ID", "BrandID");
            RenameColumn("dbo.Phototechnics", "TechnicsType_ID", "CategoryID");

            CreateIndex("dbo.Phototechnics", "CategoryID");
            CreateIndex("dbo.Phototechnics", "BrandID");

            AddForeignKey("dbo.Phototechnics", "CategoryID", "dbo.Categories", "ID");
            AddForeignKey("dbo.Phototechnics", "BrandID", "dbo.Brands", "ID");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            AddColumn("dbo.Phototechnics", "TechnicsType_ID", c => c.Guid());
            AddColumn("dbo.Phototechnics", "Manufacture_ID", c => c.Guid());
            DropForeignKey("dbo.ShopCategory", "PhotoshopID", "dbo.Photoshops");
            DropForeignKey("dbo.ShopCategory", "ID", "dbo.Categories");
            DropForeignKey("dbo.Phototechnics", "BrandID", "dbo.Brands");
            DropForeignKey("dbo.Phototechnics", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.CategoryBrand", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.CategoryBrand", "BrandID", "dbo.Brands");
            DropIndex("dbo.ShopCategory", new[] { "PhotoshopID" });
            DropIndex("dbo.ShopCategory", new[] { "ID" });
            DropIndex("dbo.Phototechnics", new[] { "BrandID" });
            DropIndex("dbo.Phototechnics", new[] { "CategoryID" });
            DropIndex("dbo.CategoryBrand", new[] { "BrandID" });
            DropIndex("dbo.CategoryBrand", new[] { "CategoryID" });
            DropColumn("dbo.Phototechnics", "BrandID");
            DropColumn("dbo.Phototechnics", "CategoryID");
            DropTable("dbo.ShopCategory");
            DropTable("dbo.Categories");
            DropTable("dbo.CategoryBrand");
            DropTable("dbo.Brands");
            CreateIndex("dbo.Phototechnics", "TechnicsType_ID");
            CreateIndex("dbo.Phototechnics", "Manufacture_ID");
            AddForeignKey("dbo.Phototechnics", "TechnicsType_ID", "dbo.Categories", "ID");
            AddForeignKey("dbo.Phototechnics", "Manufacture_ID", "dbo.Brands", "ID");
        }
    }
}
