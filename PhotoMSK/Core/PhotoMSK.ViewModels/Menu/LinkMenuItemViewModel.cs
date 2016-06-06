using System;
using AutoMapper;
using PhotoMSK.Data.Models.Menu;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Menu
{
    public static class LinkMenuItemViewModel
    {
        public class Summary:AbstractMenuItemViewModel.Summary
        {
            public string Link { get; set; }
        }

        public class Details:AbstractMenuItemViewModel.Details
        {
            public string Link { get; set; }
        }
    }

    public class LinkMenuItemViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<LinkMenuItem, LinkMenuItemViewModel.Summary>();
            CreateMap<LinkMenuItem, LinkMenuItemViewModel.Details>();
        }
    }
}
