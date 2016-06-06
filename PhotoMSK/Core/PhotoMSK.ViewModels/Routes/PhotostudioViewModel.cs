using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Route
{
    public static class PhotostudioViewModel
    {
        public class Detaills : RouteEntityViewModel.Details
        {
            public UserInformationViewModel.Summary Creator { get; set; }
            public IList<UserInformationViewModel.Summary> Administrators { get; set; }
            public object StudioType { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public string MetroStation { get; set; }
            public string Adress { get; set; }
            public IList<HallViewModel.Summary> Halls { get; set; }
            public override string RouteType => "Photostudio";

            public int MinBookingTime { get; set; }
            public int BookingTimeInc { get; set; }
            public int DaysOfClaim { get; set; }

        }

        public class Summary : RouteEntityViewModel.Summary
        {
            public string Name { get; set; }
            public string City { get; set; }
            public string MetroStation { get; set; }
            public string Adress { get; set; }
            public int HallsCount { get; set; }
            public decimal MinimumPrice { get; set; }
            public decimal HallsSquare { get; set; }
            public double? Latitude { get; set; }
            public double? Longitude { get; set; }
            public IList<HallViewModel.Descriptor> Halls { get; set; }
            public override string RouteType => "Photostudio";
        }

    }
    public class PhotostudioPrifile : MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Photostudio, PhotostudioViewModel.Summary>();
        }
    }

}