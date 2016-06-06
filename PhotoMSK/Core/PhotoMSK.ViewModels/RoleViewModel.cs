using System;
using AutoMapper;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class RoleViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
        }

        public class Details
        {
            public Guid ID { get; set; }
            public virtual UserInformationViewModel.Summary UserInformation { get; set; }
            public virtual RouteEntityViewModel.Summary Route { get; set; }
            public virtual AccessStatus AccessStatus { get; set; }
        }
    }

    public class RoleViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Role, RoleViewModel.Summary>();
            CreateMap<Role, RoleViewModel.Details>();
        }
    }
}
