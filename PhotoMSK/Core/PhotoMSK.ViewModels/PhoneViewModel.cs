using System;
using System.Collections.Generic;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class PhoneViewModel
    {
        public class Summary
        {
            public virtual Guid ID { get; set; }
            public virtual Guid EntityID { get; set; }
            public string Number { get; set; }
            public DateTime DateLastSending { get; set; }
            public bool Confirm { get; set; }
        }

        public class Details : Summary
        {
            public virtual UserPhone UserPhone { get; set; }
            public virtual ICollection<RoutesPhone> RoutesPhone { get; set; }
        }
    }

    public class PhoneViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Phone, PhoneViewModel.Summary>();
            CreateMap<Phone, PhoneViewModel.Details>();


            CreateMap<RoutesPhone, PhoneViewModel.Summary>()
                .ForMember(x => x.Number, dest => dest.MapFrom(o => o.Phone.Number))
                .ForMember(x => x.Confirm, dest => dest.MapFrom(z => z.Confirm))
                .ForMember(x => x.DateLastSending, dest => dest.MapFrom(z => z.Phone.DateLastSending));
            CreateMap<RoutesPhone, PhoneViewModel.Details>()
                .ForMember(x => x.EntityID, dest => dest.MapFrom(o => o.EntityID))
                .ForMember(x => x.Number, dest => dest.MapFrom(o => o.Phone.Number))
                .ForMember(x => x.Confirm, dest => dest.MapFrom(z => z.Confirm))
                .ForMember(x => x.DateLastSending, dest => dest.MapFrom(z => z.Phone.DateLastSending));
        }
    }
}
