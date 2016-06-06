using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.ViewModels
{
    public static class ParameterValueViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public string Value { get; set; }
        }

        public class Details
        {
            public Guid ID { get; set; }
            public virtual ParameterViewModel.Summary Parameter { get; set; }
            public virtual Phototechnics Phototechnics { get; set; }
            public string Value { get; set; }
        }
    }

    public class ParameterValueViewModelProfile:Profile
    {
        protected override void Configure()
        {
            CreateMap<ParameterValue, ParameterViewModel.Summary>();
            CreateMap<ParameterValue, ParameterViewModel.Details>();
        }
    }
}
