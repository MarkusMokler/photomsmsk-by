using System;
using System.Collections.Generic;
using System.Linq;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.Data.Models.Comments;

namespace PhotoMSK.Data.Models
{
    public class WallPost : UniqueEntity
    {
        private string _userID;

        public WallPost()
        {
            Referers = new HashSet<Views>();
        }

        public string Title { get; set; }
        public string Desctiption { get; set; }
        public DateTime Date { get; set; }
        public int Views { get; set; }

        public virtual ICollection<Views> Referers { get; set; }
        public virtual ICollection<Attachment> Attacments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual LikeStatus LikeStatus { get; set; }

        public virtual int Likes
        {
            get { return LikeStatus == null ? 0 : LikeStatus.Likes; }
        }

        public virtual int Dislikes
        {
            get { return LikeStatus == null ? 0 : LikeStatus.Dislikes; }
        }

        public virtual LikeType Opinion
        {
            get
            {
                if (string.IsNullOrEmpty(_userID))
                    return LikeType.Neutral;

                if (LikeStatus == null)
                    return LikeType.Neutral;

                Review review = LikeStatus.Reviews.SingleOrDefault(x => x.User_Id == _userID);
               
                if (review == null)
                    return LikeType.Neutral;

                return review.Like ? LikeType.Like : LikeType.Dislike;
            }
        }

        public virtual CommentList Comments { get; set; }

        public void AddLike(string accountid, LikeType likeType)
        {
            if (LikeStatus == null)
                LikeStatus = new LikeStatus {ID = Guid.NewGuid()};

            LikeDetails status = LikeStatus.AddReview(accountid, likeType);
            
            if (status == LikeDetails.NotChanged) return;

            Referers.Single(x => x.ParticipationType == ParticipationType.Owner).Entity.AddLike(likeType, status);
        }

        public virtual void SetUserID(string userid)
        {
            _userID = userid;
        }

        public void AddComment(Comment comment)
        {
            if (Comments == null)
                Comments = new CommentList {ID = Guid.NewGuid()};
            Comments.AddComment(comment);
        }
    }
}