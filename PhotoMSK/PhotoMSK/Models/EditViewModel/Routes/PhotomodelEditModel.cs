using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PhotoMSK.Areas.Admin.Models;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Models.Attributes;
using PhotoMSK.Models.EditViewModel.Interfaces;
using PhotoMSK.ViewModels;


namespace PhotoMSK.Models.EditViewModel.Routes
{
    public class PhotomodelEditModel : RouteEditModel, IFioShortcutDescription
    {
        [RequiredPost]
        [RegularExpression("[А-яA-z]{3,15}$")]
        public string FirstName { get; set; }

        [RequiredPost]
        [RegularExpression("[А-яA-z]{3,15}$")]
        public string LastName { get; set; }
   
        public int? Gender { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public double? Chest { get; set; } //сиськи 
        public double? Waist { get; set; } //пузо
        public double? Hips { get; set; } //жопа
        public double? ClothingSize { get; set; }

        public HairColor? HairColor { get; set; }
        public SkinColor? SkinColor { get; set; }
        public string City { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? HairLength { get; set; }
        public string EyesColor { get; set; }//изменить тип на enum (создать enum)
        public string FaceType { get; set; }//изменить тип на enum (создать enum)
        public int? ShoesSize { get; set; }
        public DateTime? StartYear { get; set; }
        public Article Article { get; set; }
        public IEnumerable<PhoneViewModel.Summary> Phones { get; set; }
    }

    public class PhotomodelEditModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Photomodel, PhotomodelEditModel>()
                .ForMember(x => x.Phones, dest => dest.MapFrom(z => z.Phones));
        }
    }
}