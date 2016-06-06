using System;
using AutoMapper;
using PhotoMSK.Data.Models.Menu;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Menu
{
    public static class PageMenuItemViewModel 
    {
        public class Summary:AbstractMenuItemViewModel.Summary
        {
            public Guid TextPageID { get; set; }
        }

        public class Details:AbstractMenuItemViewModel.Details
        {
            public Guid TextPageID { get; set; }
            public TextPageViewModel.Summary Page { get; set; }
        }
    }

    public class PageMenuItemViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<PageMenuItem, PageMenuItemViewModel.Summary>();
            CreateMap<PageMenuItem, PageMenuItemViewModel.Details>();
        }
    }
}
