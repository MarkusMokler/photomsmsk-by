using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class UserPhoneViewModel
    {
        public class Summary
        {
            public virtual Guid ID { get; set; }
            public virtual Guid UserID { get; set; }
            public virtual bool Confirm { get; set; }
            public virtual int ConfirmCode { get; set; }
            public virtual DateTime DateAdded { get; set; }

        }

        public class Details
        {
            public virtual Guid ID { get; set; }
            public virtual Guid UserID { get; set; }
            public virtual UserInformationViewModel.Summary User { get; set; }
            public virtual PhoneViewModel.Summary Phone { get; set; }
            public virtual bool Confirm { get; set; }
            public virtual int ConfirmCode { get; set; }
            public virtual DateTime DateAdded { get; set; }
        }
    }

    public class UserPhoneViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<UserPhone, UserPhoneViewModel.Summary>();
            CreateMap<UserPhone, UserPhoneViewModel.Details>();
        }
    }
}
