using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Routes;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class PhotographerCalendarViewModel
    {
        public class Summary : CalendarViewModel
        {

        }

        public class Details : CalendarViewModel
        {
            public virtual PhotographerViewModel.Details Photographer { get; set; }
            public RouteEntityViewModel.Summary RouteEntity { get; set; }
        }
    }

    public class PhotographerCalendarViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<PhotographerCalendar, PhotographerCalendarViewModel.Summary>();
            CreateMap<PhotographerCalendar, PhotographerCalendarViewModel.Details>();
        }
    }
}
