using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Models.Attributes;

namespace PhotoMSK.Models.EditViewModel.Routes
{
    public class PhotostudioEditModel : RouteEditModel
    {
        [RequiredPost]
        [RegularExpression("[А-яA-z 0-9]{3,25}$")]
        public string Name { get; set; }
        public string City { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string MetroStation { get; set; }
        public string Adress { get; set; }
        public string StudioType { get; set; }

        public IEnumerable<HallEditModel> Halls { get; set; }
    }

    public class PhotostudioEditModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Photostudio, PhotostudioEditModel>();
        }
    }
}