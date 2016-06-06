using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class PromocodeViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }            
            public bool Active { get; set; }
            public double Percentage { get; set; }
            public DateTime? ExpiryDate { get; set; }
        }

        public class Details
        {
            public Guid ID { get; set; }
            public Guid Code { get; set; }
            public bool Active { get; set; }
            public double Percentage { get; set; }
            public DateTime? ExpiryDate { get; set; }
            public RouteEntityViewModel.Summary OwnerEntity { get; set; } 
        }
    }

    public class PromocodeViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Promocode, PromocodeViewModel.Summary>();
            CreateMap<Promocode, PromocodeViewModel.Details>();
        }
    }
}
