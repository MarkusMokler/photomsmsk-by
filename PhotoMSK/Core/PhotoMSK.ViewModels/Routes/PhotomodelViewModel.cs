using System;
using System.Collections.Generic;
using AutoMapper;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.ViewModels
{
    public static class PhotomodelViewModel
    {
        public class Summary : RouteEntityViewModel.Summary
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Gender { get; set; }
            public double Height { get; set; }
            public double Weight { get; set; }
            public double Chest { get; set; } //сиськи 
            public double Waist { get; set; } //пузо
            public double Hips { get; set; } //жопа
            public double ClothingSize { get; set; }

            public HairColor HairColor { get; set; }
            public SkinColor SkinColor { get; set; }
            public string City { get; set; }
            public int Age { get; set; }
            public int HairLength { get; set; }
            public string EyesColor { get; set; }
            public string FaceType { get; set; }
            public int ShoesSize { get; set; }
            public DateTime StartYear { get; set; }
            public override string RouteType => "Photomodel";
        }

        public class Details : RouteEntityViewModel.Details
        {
            public override string RouteType => "Photomodel";

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Gender { get; set; }
            public double Height { get; set; }
            public double Weight { get; set; }
            public double Chest { get; set; } //сиськи 
            public double Waist { get; set; } //пузо
            public double Hips { get; set; } //жопа
            public double ClothingSize { get; set; }

            public HairColor HairColor { get; set; }
            public SkinColor SkinColor { get; set; }
            public string City { get; set; }
            public int Age { get; set; }
            public int HairLength { get; set; }
            public string EyesColor { get; set; }
            public string FaceType { get; set; }
            public int ShoesSize { get; set; }
            public DateTime StartYear { get; set; }
            public virtual ICollection<GenreViewModel.Summary> Genres { get; set; }
            public virtual ICollection<SnapshotViewModel.Details> Snapshots { get; set; }
            public virtual Article Article { get; set; }
        }

    }
}