using System;
using System.Collections.Generic;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class GenreViewModel
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
            public virtual ICollection<PhotomodelViewModel.Summary> Photomodels { get; set; }
        }
    }

    public class GenreViewModelViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Genre, GenreViewModel.Summary>();
            CreateMap<Genre, GenreViewModel.Details>();
        }
    }
}
