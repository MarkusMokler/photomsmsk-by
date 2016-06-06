using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class RentCalendarViewModel
    {
        public class Summary : CalendarViewModel
        {
            public virtual Guid PhotorentID { get; set; }
            public virtual Guid PhototechnicsID { get; set; }
            public double HourlyPrice { get; set; }
            public double HalfDayPrice { get; set; }
            public double DaylyPrice { get; set; }
            public double HollidayPrice { get; set; }
            public double WeeklyPrice { get; set; }
            public double MonthlyPrice { get; set; }
        }

        public class Details : CalendarViewModel
        {
            public virtual Guid PhotorentID { get; set; }
            public virtual Guid PhototechnicsID { get; set; }
            public double HourlyPrice { get; set; }
            public double HalfDayPrice { get; set; }
            public double DaylyPrice { get; set; }
            public double HollidayPrice { get; set; }
            public double WeeklyPrice { get; set; }
            public double MonthlyPrice { get; set; }

            public virtual PhotorentViewModel.Details Photorent { get; set; }
            public virtual PhototechnicsViewModel.Summary Phototechnics { get; set; }
        }
    }

    public class RentCalendarViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<RentCalendar, RentCalendarViewModel.Summary>();
            CreateMap<RentCalendar, RentCalendarViewModel.Details>();
        }
    }
}
