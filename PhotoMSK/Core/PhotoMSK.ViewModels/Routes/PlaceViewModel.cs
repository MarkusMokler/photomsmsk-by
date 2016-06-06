using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.ViewModels.Route
{
    public static class PlaceViewModel
    {
        public class Summary
        {
            public string Title { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }

        public class Details
        {
            public string Title { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }
    }
}
