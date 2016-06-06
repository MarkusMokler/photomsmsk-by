namespace PhotoMSK.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotomodelNullableFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Photomodel", "Gender", c => c.Int());
            AlterColumn("dbo.Photomodel", "HairColor", c => c.Int());
            AlterColumn("dbo.Photomodel", "SkinColor", c => c.Int());
            AlterColumn("dbo.Photomodel", "EyesColor", c => c.Int());
            AlterColumn("dbo.Photomodel", "FaceType", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Photomodel", "FaceType", c => c.Int(nullable: false));
            AlterColumn("dbo.Photomodel", "EyesColor", c => c.Int(nullable: false));
            AlterColumn("dbo.Photomodel", "SkinColor", c => c.Int(nullable: false));
            AlterColumn("dbo.Photomodel", "HairColor", c => c.Int(nullable: false));
            AlterColumn("dbo.Photomodel", "Gender", c => c.Int(nullable: false));
        }
    }
}
