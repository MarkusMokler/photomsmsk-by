using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class ScheduleDayViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public int DayOfWeek { get; set; }
            public DateTime TimeStart { get; set; }
            public DateTime TimeEnd { get; set; }
            public double Price { get; set; }
            public Guid HallID { get; set; }
        }

        public class Details
        {
            public Guid ID { get; set; }
            public int DayOfWeek { get; set; }
            public DateTime TimeStart { get; set; }
            public DateTime TimeEnd { get; set; }
            public double Price { get; set; }
            public Guid HallID { get; set; }
            public virtual HallViewModel.Summary Hall { get; set; }
        }
    }

    public class ScheduleDayViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<ScheduleDay, ScheduleDayViewModel.Summary>();
            CreateMap<ScheduleDay, ScheduleDayViewModel.Details>();
        }
    }
}
