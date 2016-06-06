using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static partial class CategoryBrandViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
        }
    }

    public class CategoryBrandViewModelSummaryProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<CategoryBrand, CalendarReferenceViewModel.Summary>();
            CreateMap<CategoryBrand, CalendarReferenceViewModel.Details>();
        }
    }
}
