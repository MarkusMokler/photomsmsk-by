using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.Data.Models
{
    public class LikeStatus : UniqueEntity
    {
        public virtual WallPost WallPost { get; set; }
        public virtual Creative Creative { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual RouteReview RouteReviews { get; set; } 
        public int Likes { get; protected set; }
        public int Dislikes { get; protected set; }

        public virtual bool AddReview(Review review)
        {
            if (Reviews == null)
                Reviews = new Collection<Review>();

            if (Reviews.Any(x => x.User.Id == review.User.Id))
                return false;

            if (review.Like)
                Likes++;
            else
                Dislikes++;

            Reviews.Add(review);
            return true;
        }

        public LikeDetails AddReview(string accountid, LikeType likeType)
        {
            if (Reviews == null)
                Reviews = new Collection<Review>();

            Review obj = Reviews.SingleOrDefault(x => x.User.Id == accountid);
            if (obj == null)
            {
                var r = new Review {ID = Guid.NewGuid(), Like = likeType == LikeType.Like, User_Id = accountid};
                Reviews.Add(r);
                if (r.Like)
                    Likes++;
                else
                    Dislikes++;
                return LikeDetails.Voited;
            }

            if (obj.Like && likeType == LikeType.Dislike)
            {
                Likes--;
                Dislikes++;
                obj.Like = false;
                return LikeDetails.Changed;
            }

            if (!obj.Like && likeType == LikeType.Like)
            {
                Likes++;
                Dislikes--;
                obj.Like = true;
                return LikeDetails.Changed;
            }
            return LikeDetails.NotChanged;
        }

        public class LikeStatusConfiguration : EntityTypeConfiguration<LikeStatus>
        {
            public LikeStatusConfiguration()
            {
                Property(p => p.Likes);
                Property(p => p.Dislikes);
                HasOptional(x => x.WallPost).WithOptionalPrincipal(x => x.LikeStatus);
                HasOptional(x => x.Creative).WithOptionalPrincipal(x => x.LikeStatus);
                HasOptional(x => x.RouteReviews).WithOptionalPrincipal(x => x.LikeStatus);
            }
        }
    }
}