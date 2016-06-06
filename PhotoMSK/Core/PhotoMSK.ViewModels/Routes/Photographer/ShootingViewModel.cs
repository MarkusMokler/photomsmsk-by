using System;
using System.Collections.Generic;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class ShootingViewModel
    {
        public class Summary : WallPostViewModel.Summary
        {
            public bool CanEdit
            {
                get { return DateTime.Now.AddDays(-7) < Date; }
            }
        }

        public class Details : WallPostViewModel.Details
        {
            public bool CanEdit
            {
                get { return DateTime.Now.AddDays(-7) < Date; }
            }

            public virtual List<ShootingGenre> Geners { get; set; }
        }


    }

    public class ShootingViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Shooting, ShootingViewModel.Summary>();
            CreateMap<Shooting, ShootingViewModel.Details>();
        }
    }
}
