using System;
using System.Collections.Generic;
using PhotoMSK.Data.Enums;

namespace PhotoMSK.Data.Models.Routes
{
    public class Photomodel : RouteEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double Chest { get; set; } //сиськи 
        public double Waist { get; set; } //пузо
        public double Hips { get; set; } //жопа
        public double ClothingSize { get; set; }

        public HairColor? HairColor { get; set; }
        public SkinColor? SkinColor { get; set; }
        public MartialStatus MartialStatus { get; set; }
        public virtual ICollection<Snapshot> Snapshots { get; set; }
        public virtual Article Article { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }
        public int HairLength { get; set; }
        public EyesColor? EyesColor { get; set; }
        public FaceType? FaceType { get; set; }
        public int ShoesSize { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
        public DateTime StartYear { get; set; }

        public override string ToString()
        {
            return LastName + " " + FirstName;
        }

        public override string GetName()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}