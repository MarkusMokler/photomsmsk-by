using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class SaleCardViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public Guid UserInformationID { get; set; }
            public string CardNumber { get; set; }
            public string Pin { get; set; }
            public DateTime ExpiredDate { get; set; }
        }

        public class Details
        {
            public Guid ID { get; set; }
            public Guid UserInformationID { get; set; }
            public virtual UserInformationViewModel.Summary UserInformation { get; set; }
            public string CardNumber { get; set; }
            public string Pin { get; set; }
            public DateTime ExpiredDate { get; set; }
        }
    }

    public class SaleCardViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<SaleCard, SaleCardViewModel.Summary>();
            CreateMap<SaleCard, SaleCardViewModel.Details>();
        }
    }
}
