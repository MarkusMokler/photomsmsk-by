using System;
using AutoMapper;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Route
{
    public class BrandViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

    }

    public class BrandViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {

            CreateMap<Brand, BrandViewModel>();
        }
    }

}