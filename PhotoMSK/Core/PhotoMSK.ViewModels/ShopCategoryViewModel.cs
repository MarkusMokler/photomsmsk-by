using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Routes;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public class ShopCategoryViewModel
    {
        public class Summary : CategoryViewModel
        {
            public Guid PhotoshopID { get; set; }
            public int Order { get; set; }
        }

        public class Details : CategoryViewModel
        {
            public PhotoshopViewModel.Summary Photoshop { get; set; }
            public Guid PhotoshopID { get; set; }
            public int Order { get; set; }
        }
    }

    public class ShopCategoryViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<ShopCategory, ShopCategoryViewModel.Summary>();
            CreateMap<ShopCategory, ShopCategoryViewModel.Details>();
        }
    }
}