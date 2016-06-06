using System.Collections.Generic;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class UserViewModel
    {
        public class Summary
        {
            
        }

        public class Details
        {
            public virtual UserInformationViewModel.Summary UserInformation { get; set; }
            public virtual ICollection<UserMenuItemViewModel.Summary> UserMenuItems { get; set; }
            public virtual ICollection<AdresseViewModel.Summary> SendedMessages { get; set; }
            public virtual ICollection<AdresseViewModel.Summary> RecivedMessages { get; set; }
        }
    }

    public class UserViewModelViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<User, UserViewModel.Summary>();
            CreateMap<User, UserViewModel.Details>();
        }
    }
}
