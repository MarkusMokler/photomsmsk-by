using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.RoutePageLayouts
{
    public class LayoutViewModel
    {
        public class Summary
        {
            public string Name { get; set; }
            public string Content { get; set; }
            public string Zones { get; set; }
        }
        public class Details
        {

        }
    }

    public class LayoutViewModelProfile : MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Layout, LayoutViewModel.Summary>()
                .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Name))
                .ForMember(dest => dest.Content, source => source.MapFrom(mem => mem.Content))
                .ForMember(dest => dest.Zones, source => source.MapFrom(mem => mem.Zones));
        }
    }
}
