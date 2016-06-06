using System;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models.Comments
{
    public class Comment
    {
        public Guid ID { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid UserInformationID { get; set; }
        public virtual UserInformation User { get; set; }
        public Guid? AnsweredUserInformationID { get; set; }
        public virtual UserInformation AnsweredUser { get; set; }
        public virtual Guid CommentsID { get; set; }
        public virtual CommentList Comments { get; set; }

        public bool IsAnswer
        {
            get { return AnsweredUserInformationID != null; }
        }
    }

    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            HasKey(x => x.ID);
            HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserInformationID);
            HasOptional(x => x.AnsweredUser).WithMany().HasForeignKey(x => x.AnsweredUserInformationID);
            HasRequired(x => x.Comments).WithMany(x => x.Comments).HasForeignKey(x => x.CommentsID);
        }
    }
}