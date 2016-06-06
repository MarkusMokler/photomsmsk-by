using System;
using System.Collections.Generic;
using AutoMapper;
using PhotoMSK.Data.Models.Advertising;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class AdCompanyViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public string Name { get; set; }
        }

        public class Details
        {
            public Guid ID { get; set; }
            public string Name { get; set; }
            public virtual ICollection<AdresseViewModel.Summary> Ads { get; set; }
        }
    }

    public class AdCompanyViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<AdCompany,AdCompanyViewModel.Summary>();
            CreateMap<AdCompany, AdCompanyViewModel.Details>();
        }
    }
}
