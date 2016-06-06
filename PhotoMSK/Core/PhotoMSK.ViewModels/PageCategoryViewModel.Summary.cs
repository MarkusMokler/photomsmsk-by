using System;
using System.Collections.Generic;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static partial class PageCategoryViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public string CategoryName { get; set; }
            public string Slug { get; set; }
        }
        
        public class Details
        {
            public Guid ID { get; set; }
            public Guid RouteID { get; set; }
            public virtual RouteEntityViewModel.Summary Route { get; set; }
            public string CategoryName { get; set; }
            public string Slug { get; set; }
            public ICollection<TextPageViewModel.Summary> Pages { get; set; }
        }
    }

    public class PageCategoryViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<PageCategory, PageCategoryViewModel.Summary>();
            CreateMap<PageCategory, PageCategoryViewModel.Details>();
        }
    }
}
