using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Routes;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class PhotoGenerViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public int Level { get; set; }
        }

        public class Details
        {
            public Guid ID { get; set; }
            public virtual PhotographerViewModel.Summary Photographer { get; set; }
            public virtual GenreViewModel.Summary Genre { get; set; }
            public int Level { get; set; }
        }
    }

    public class PhotoGenerViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<PhotoGener, PhotoGenerViewModel.Summary>();
            CreateMap<PhotoGener, PhotoGenerViewModel.Details>();
        }
    }
}
