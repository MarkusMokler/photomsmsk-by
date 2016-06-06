using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static partial class HallPropertyViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public string Name { get; set; }
            public int Group { get; set; }
            public int Index { get; set; }
        }
    }

    public class HallPropertyViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<HallProperty, HallPropertyViewModel.Summary>();
            CreateMap<HallProperty, HallPropertyViewModel.Details>();
        }
    }
}
