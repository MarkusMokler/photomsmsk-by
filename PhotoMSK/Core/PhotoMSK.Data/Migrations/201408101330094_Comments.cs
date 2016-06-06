using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class Comments : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.News", newName: "WallPosts");
            RenameColumn(table: "dbo.Attachments", name: "News_ID", newName: "WallPost_ID");
            RenameColumn(table: "dbo.Views", name: "News_ID", newName: "WallPost_ID");
            RenameColumn(table: "dbo.Reviews", name: "News_ID", newName: "WallPost_ID");
            RenameIndex(table: "dbo.Attachments", name: "IX_News_ID", newName: "IX_WallPost_ID");
            RenameIndex(table: "dbo.Reviews", name: "IX_News_ID", newName: "IX_WallPost_ID");
            RenameIndex(table: "dbo.Views", name: "IX_News_ID", newName: "IX_WallPost_ID");
            CreateTable(
                "dbo.CommentLists",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CommentsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Text = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        UserInformationID = c.Guid(nullable: false),
                        AnsweredUserInformationID = c.Guid(),
                        CommentsID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserInformations", t => t.AnsweredUserInformationID)
                .ForeignKey("dbo.CommentLists", t => t.CommentsID, cascadeDelete: true)
                .ForeignKey("dbo.UserInformations", t => t.UserInformationID, cascadeDelete: true)
                .Index(t => t.UserInformationID)
                .Index(t => t.AnsweredUserInformationID)
                .Index(t => t.CommentsID);
            
            AddColumn("dbo.WallPosts", "Comments_ID", c => c.Guid());
            CreateIndex("dbo.WallPosts", "Comments_ID");
            AddForeignKey("dbo.WallPosts", "Comments_ID", "dbo.CommentLists", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WallPosts", "Comments_ID", "dbo.CommentLists");
            DropForeignKey("dbo.Comments", "UserInformationID", "dbo.UserInformations");
            DropForeignKey("dbo.Comments", "CommentsID", "dbo.CommentLists");
            DropForeignKey("dbo.Comments", "AnsweredUserInformationID", "dbo.UserInformations");
            DropIndex("dbo.Comments", new[] { "CommentsID" });
            DropIndex("dbo.Comments", new[] { "AnsweredUserInformationID" });
            DropIndex("dbo.Comments", new[] { "UserInformationID" });
            DropIndex("dbo.WallPosts", new[] { "Comments_ID" });
            DropColumn("dbo.WallPosts", "Comments_ID");
            DropTable("dbo.Comments");
            DropTable("dbo.CommentLists");
            RenameIndex(table: "dbo.Views", name: "IX_WallPost_ID", newName: "IX_News_ID");
            RenameIndex(table: "dbo.Reviews", name: "IX_WallPost_ID", newName: "IX_News_ID");
            RenameIndex(table: "dbo.Attachments", name: "IX_WallPost_ID", newName: "IX_News_ID");
            RenameColumn(table: "dbo.Reviews", name: "WallPost_ID", newName: "News_ID");
            RenameColumn(table: "dbo.Views", name: "WallPost_ID", newName: "News_ID");
            RenameColumn(table: "dbo.Attachments", name: "WallPost_ID", newName: "News_ID");
            RenameTable(name: "dbo.WallPosts", newName: "News");
        }
    }
}
