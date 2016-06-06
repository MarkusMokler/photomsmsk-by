using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static partial class UserMenuItemViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public int Index { get; set; }
            public string Icon { get; set; }
            public string Action { get; set; }
            public string Controller { get; set; }
            public string Title { get; set; }
            public string Shortcut { get; set; }
        }
    }
    public class UserMenuItemViewModelSummaryProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<UserMenuItem, UserMenuItemViewModel.Summary>();
            CreateMap<UserMenuItem, UserMenuItemViewModel.Details>();
        }
    }
}
