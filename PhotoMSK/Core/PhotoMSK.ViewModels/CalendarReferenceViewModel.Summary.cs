using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static partial class CalendarReferenceViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public string CopyFrom { get; set; }
            public DateTime LastCollectTime { get; set; }
            public string Color { get; set; }
        }
    }

    public class CalendarReferenceSummaryProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<CalendarReference, CalendarReferenceViewModel.Summary>();
            CreateMap<CalendarReference, CalendarReferenceViewModel.Details>();
        }
    }
}
