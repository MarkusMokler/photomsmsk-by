using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoMSK.Data.Models.Comments
{
    public class CommentList
    {
        public Guid ID { get; set; }
        public int CommentsCount { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public void AddComment(Comment c)
        {
            if (Comments == null)
                Comments = new List<Comment>();
            Comments.Add(c);
            CommentsCount++;
        }

        public ICollection<Comment> GetLastComments(int count = 3)
        {
            return Comments.OrderByDescending(x => x.CreationTime).Take(count).ToList();
        }
    }
}