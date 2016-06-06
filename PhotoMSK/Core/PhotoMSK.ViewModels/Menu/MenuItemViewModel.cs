using System;
using System.Collections.Generic;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Menu;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Route
{
    public class MenuItemViewModel
    {
        public Guid ID { get; set; }
        public String Name { get; set; }
        public String Order { get; set; }
        public string Link { get; set; }
        public BasePage BasePage { get; set; }
        public bool Publish { get; set; }
        public Guid RouteID { get; set; }
        public List<MenuItemViewModel> Nodes { get; set; } = new List<MenuItemViewModel>();
        public string Type { get; set; }
        public Guid? ParentID { get; set; }

        public void Add(MenuItemViewModel children)
        {
            Nodes.Add(children);
        }
    }

    public class MenuItemViewModelProfile : MapperProfile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AbstractMenuItem, MenuItemViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Order, y => y.MapFrom(z => z.Order))
                .ForMember(x => x.Publish, y => y.MapFrom(z => z.Publish))
                .ForMember(x => x.ID, y => y.MapFrom(z => z.ID))
                .ForMember(x => x.RouteID, y => y.MapFrom(z => z.RouteID))
                .ForMember(x => x.Type, y => y.MapFrom(z => GetMenuType(z)))
                 .ForMember(x => x.Nodes, y => y.Ignore())
                .Include<LinkMenuItem, MenuItemViewModel>()
                .Include<PageMenuItem, MenuItemViewModel>();

            Mapper.CreateMap<LinkMenuItem, MenuItemViewModel>().ForMember(x => x.Link, y => y.MapFrom(z => z.Link));
            Mapper.CreateMap<PageMenuItem, MenuItemViewModel>().ForMember(x => x.BasePage, y => y.MapFrom(z => z.BasePage));
        }

        private static string GetMenuType(AbstractMenuItem z)
        {
            if (z is LinkMenuItem) return "LinkMenuItem";
            if (z is PageMenuItem) return "PageMenuItem";
            if (z is RouteMenu) return "RouteMenu";

            throw new NotImplementedException("Not Implemented types");
        }
    }
}