using System;
using AutoMapper;
using PhotoMSK.Data.Models.Advertising;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Advertising
{
    public static class AdViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public String Title { get; set; }
            public string Subtitle { get; set; }
            public string Image { get; set; }
            public string Description { get; set; }
            public int Views { get; set; }
            public int Visits { get; set; }
            public string Link { get; set; }
        }

        public class Details
        {
            public Guid ID { get; set; }
            public String Title { get; set; }
            public string Subtitle { get; set; }
            public string Image { get; set; }
            public string Description { get; set; }
            public int Views { get; set; }
            public int Visits { get; set; }
            public string Link { get; set; }
            public virtual AdCompanyViewModel.Summary Company { get; set; }
        }
    }

    public class AdViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Ad,AdViewModel.Summary>();
            CreateMap<Ad, AdViewModel.Details>();
        }
    }
}
