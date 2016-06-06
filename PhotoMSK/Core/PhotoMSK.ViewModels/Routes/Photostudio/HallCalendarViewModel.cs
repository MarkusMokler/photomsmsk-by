using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class HallCalendarViewModel
    {
        public class Summary : CalendarViewModel
        {

        }

        public class Details : CalendarViewModel
        {
            public virtual HallViewModel.Summary Hall { get; set; }

        }
    }

    public class HallCalendarViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<HallCalendar, HallCalendarViewModel.Summary>();
            CreateMap<HallCalendar, HallCalendarViewModel.Details>();
        }
    }
}
