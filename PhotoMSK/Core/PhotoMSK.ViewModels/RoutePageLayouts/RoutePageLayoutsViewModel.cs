using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Social;
using PhotoMSK.ViewModels.Widgets;

namespace PhotoMSK.ViewModels.RoutePageLayouts
{
    public class RoutePageLayoutsViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public string Name { get; set; }
            public Guid LayoutId { get; set; }
            public virtual Layout Layout { get; set; }
            public virtual ICollection<ZoneViewModel> Zones { get; set; }
            public string Category { get; set; }
            public RouteEntityViewModel.Summary Route { get; set; }
            public virtual ThemeStyleViewModel Style { get; set; }

        }
        public class Details
        {
            public Guid ID { get; set; }
            public string Name { get; set; }
            public virtual Layout Layout { get; set; }
            public virtual ICollection<ZoneViewModel> Zones { get; set; }
            public virtual ThemeStyleViewModel Style { get; set; }
        }
        public class Named
        {
            public Guid ID { get; set; }
            public string Name { get; set; }
            public Guid LayoutId { get; set; }
            public virtual ThemeStyleViewModel Style { get; set; }
        }
    }

    public class RoutePageLayoutsViewModelProfile : MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<RoutepageLayout, RoutePageLayoutsViewModel.Summary>();
            CreateMap<RoutepageLayout, RoutePageLayoutsViewModel.Details>();
            CreateMap<RoutepageLayout, RoutePageLayoutsViewModel.Named>();
        }
    }
}
