using System;
using System.Data.Entity.ModelConfiguration;
using PhotoMSK.Data.Enums;

namespace PhotoMSK.Data.Models
{
    public class Raiting : UniqueEntity
    {
        public virtual RouteEntity Entity { get; set; }
        public int Views { get; protected set; }
        public int Likes { get; protected set; }
        public int Dislikes { get; protected set; }
        public int WallPosts { get; protected set; }
        public int Comments { get; protected set; }
        public int Payed { get; protected set; }
        public double Total { get; protected set; }

        public virtual void AddView()
        {
            Views++;
            UpdateTotal();
        }

        public virtual void AddLike(LikeDetails exist)
        {
            Likes++;
            if (exist == LikeDetails.Changed)
                Dislikes--;
            UpdateTotal();
        }

        public virtual void AddDislike(LikeDetails b)
        {
            Dislikes++;
            if (b == LikeDetails.Changed)
                Likes--;
            UpdateTotal();
        }

        public virtual void AddWallPost()
        {
            WallPosts++;
            UpdateTotal();
        }

        public virtual void AddComment()
        {
            Comments++;
            UpdateTotal();
        }

        public virtual void AddPayed()
        {
            Payed++;
            UpdateTotal();
        }

        public virtual void UpdateTotal()
        {
            Total = Views/10.0 + (Likes/(Dislikes + 1.0)/(WallPosts + 1.0)) + Comments/(WallPosts + 1.0) + Payed;
        }
    }

    public class RaitingConfiguration : EntityTypeConfiguration<Raiting>
    {
        public RaitingConfiguration()
        {
            HasRequired(x => x.Entity).WithRequiredDependent(x => x.Raiting);
        }
    }
}