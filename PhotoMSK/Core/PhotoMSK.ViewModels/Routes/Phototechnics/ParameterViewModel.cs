using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Route
{
    public class ParameterViewModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ParameterViewModelPrifile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<ParameterValue, ParameterViewModel>()
                .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Parameter.Name))
                .ForMember(dest => dest.Value, source => source.MapFrom(mem => mem.Value));

        }
    }
}