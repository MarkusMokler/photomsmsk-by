using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class ShootingGenreViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public Guid GenreID { get; set; }
            public Guid ShootingID { get; set; }
        }

        public class Details
        {
            public Guid ID { get; set; }
            public GenreViewModel.Summary Genre { get; set; }
            public Shooting Shooting { get; set; }
            public Guid GenreID { get; set; }
            public Guid ShootingID { get; set; }
        }
    }

    public class ShootingGenreViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<ShootingGenre, ShootingGenreViewModel.Summary>();
            CreateMap<ShootingGenre, ShootingGenreViewModel.Details>();
        }
    }
}
