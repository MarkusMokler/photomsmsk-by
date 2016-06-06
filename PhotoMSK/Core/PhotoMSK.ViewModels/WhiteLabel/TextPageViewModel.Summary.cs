using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Pages;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static partial class TextPageViewModel
    {
        public class Summary
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Slug { get; set; }
            public string Text { get; set; }

            public string Keywords { get; set; }
            public string Description { get; set; }

            public bool Published { get; set; }
            public bool Comment { get; set; }
            public string PageType { get; set; }
            public Guid? PageLayoutID { get; set; }
        }
    }

    public class TextPageViewModelSummaryProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<BasePage, TextPageViewModel.Summary>();
            CreateMap<BasePage, TextPageViewModel.Details>();


            CreateMap<ContentPage, TextPageViewModel.Summary>();
            CreateMap<ContentPage, TextPageViewModel.Details>();

            CreateMap<ProjectionPage, TextPageViewModel.Summary>();
            CreateMap<ProjectionPage, TextPageViewModel.Details>();


            CreateMap<LandingPage, TextPageViewModel.Summary>();
            CreateMap<LandingPage, TextPageViewModel.Details>();
        }
    }
}