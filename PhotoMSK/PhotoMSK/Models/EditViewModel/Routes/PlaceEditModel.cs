using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Models.Attributes;


namespace PhotoMSK.Models.EditViewModel.Routes
{
    public class PlaceEditModel : RouteEditModel
    {
        [RequiredPost]
        public string Title { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
    public class PlaceEditModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Place, PlaceEditModel>();
        }
    }
}