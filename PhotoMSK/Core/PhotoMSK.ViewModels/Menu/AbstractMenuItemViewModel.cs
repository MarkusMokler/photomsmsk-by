using System;
using AutoMapper;
using PhotoMSK.Data.Models.Menu;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Menu
{
    public abstract class AbstractMenuItemViewModel
    {
        public abstract class Summary
        {
            public Guid ID { get; set; }
            public Guid RouteID { get; set; }
            public String Name { get; set; }
            public String Order { get; set; }
            public bool Publish { get; set; }
        }

        public abstract class Details
        {
            public Guid ID { get; set; }
            public Guid RouteID { get; set; }
            public virtual RouteEntityViewModel.Summary RouteEntity { get; set; }
            public String Name { get; set; }
            public String Order { get; set; }
            public bool Publish { get; set; }
        }
    }

    public class AbstractMenuItemViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<AbstractMenuItem, AbstractMenuItemViewModel.Summary>();
            CreateMap<AbstractMenuItem, AbstractMenuItemViewModel.Details>();
        }
    }
}
