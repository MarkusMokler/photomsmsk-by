using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class ParameterViewModel
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
        }
    }

    public class ParameterViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Parameter, ParameterViewModel.Summary>();
            CreateMap<Parameter, ParameterViewModel.Details>();
        }
    }
}
