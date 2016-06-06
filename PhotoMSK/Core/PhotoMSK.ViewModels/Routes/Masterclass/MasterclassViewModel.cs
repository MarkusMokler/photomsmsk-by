using System;
using System.Collections.Generic;
using AutoMapper;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Route
{
    public class MasterclassViewModel
    {
        public class Summary : RouteEntityViewModel.Summary
        {
            public string Title { get; set; }
            public DateTime StartDay { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public int Days { get; set; }
            public string Listeners { get; set; }
            public string AuthorName { get; set; }
            public override string RouteType => "Masterclass";
        }

        public class Details : RouteEntityViewModel.Details
        {
            public string Title { get; set; }
            public DateTime StartDay { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public int Days { get; set; }
            public string Listeners { get; set; }
            public string AuthorName { get; set; }
            public virtual ICollection<MasterclassEventsViewModel.Summary> Events { get; set; }
            public override string RouteType => "Masterclass";

        }
    }

    public class MasterclassViewModelProfile : MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Masterclass, MasterclassViewModel.Summary>();
            CreateMap<Masterclass, MasterclassViewModel.Details>();
        }
    }
}
