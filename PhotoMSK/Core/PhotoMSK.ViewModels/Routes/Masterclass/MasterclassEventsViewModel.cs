using System;
using AutoMapper;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Route
{
    public static class MasterclassEventsViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public DateTime Time { get; set; }
            public MasterEventType EventType { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }

        public class Details
        {
            public Guid ID { get; set; }
            public DateTime Time { get; set; }
            public MasterEventType EventType { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }
    }

    public class MasterclassEventsViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<MasterclassEvents, MasterclassEventsViewModel.Summary>();
            CreateMap<MasterclassEvents, MasterclassEventsViewModel.Details>();
        }
    }
}
