using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PhotoMSK.Data.Models.Menu;
using PhotoMSK.ViewModels.Event;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Menu
{
    public class RouteMenuViewModel
    {
        public class Details : AbstractMenuItemViewModel.Summary
        {

        }
    }
    public class RouteMenuViewModelMapper : MapperProfile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AbstractMenuItem, RouteMenuViewModel.Details>()
                 .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Order, y => y.MapFrom(z => z.Order))
                .ForMember(x => x.Publish, y => y.MapFrom(z => z.Publish));
        }
    }
}

