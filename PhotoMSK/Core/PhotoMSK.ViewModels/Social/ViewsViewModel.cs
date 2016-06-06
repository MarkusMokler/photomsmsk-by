using System;
using AutoMapper;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class ViewsViewModel
    {
        public class Summary
        {
            public virtual Guid ID { get; set; }
            public Guid EntityID { get; set; }
            public DateTime Date { get; set; }
        }

        public class Details
        {
            public virtual Guid ID { get; set; }
            public virtual WallPostViewModel.Summary WallPost { get; set; }
            public Guid EntityID { get; set; }
            public virtual RouteEntityViewModel.Summary Entity { get; set; }
            public ParticipationType ParticipationType { get; set; }
            public DateTime Date { get; set; }
        }
    }

    public class ViewsViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Views, ViewsViewModel.Summary>();
            CreateMap<Views, ViewsViewModel.Details>();
        }
    }
}
