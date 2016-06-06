using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class RoutesPhoneViewModel
    {
        public class Summary
        {
            public virtual Guid ID { get; set; }
            public virtual Guid EntityID { get; set; }
            public virtual Guid PhoneID { get; set; }
            public virtual bool Confirm { get; set; }
            public virtual int ConfirmCode { get; set; }
            public virtual DateTime DateAdded { get; set; }
        }

        public class Details
        {
            public virtual Guid ID { get; set; }
            public virtual Guid EntityID { get; set; }
            public virtual RouteEntityViewModel.Summary Entity { get; set; }
            public virtual Guid PhoneID { get; set; }
            public virtual PhoneViewModel.Summary Phone { get; set; }
            public virtual bool Confirm { get; set; }
            public virtual int ConfirmCode { get; set; }
            public virtual DateTime DateAdded { get; set; }
            public IList<string> Phones { get; set; } 
        }
    }

    public class RoutesPhoneViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<RoutesPhone, RoutesPhoneViewModel.Summary>();
            CreateMap<RoutesPhone, RoutesPhoneViewModel.Details>()
                .ForMember(dest => dest.Phone, source => source.MapFrom(x => x.Phone))
                .ForMember(dest => dest.Phones, source => source.MapFrom(x => x.Entity.Phones.Select(z => z.Phone.Number)));

        }
    }
}
