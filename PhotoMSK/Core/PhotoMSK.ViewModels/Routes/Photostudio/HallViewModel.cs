using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Attachments;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class HallViewModel
    {
        public class Details : Summary
        {
            public IEnumerable<PhotoViewModel.Summary> Photos { get; set; }
            public virtual HallCalendarViewModel.Summary Calendar { get; set; }
            public IList<ScheduleDayViewModel.Summary> Schedule { get; set; }
            public virtual PhotostudioViewModel.Summary Photostudio { get; set; }
        }

        public class Summary : Descriptor
        {
            public Guid CalendarID { get; set; }
            public Guid? LandingPageID { get; set; }
            public string TeaserImageUrl { get; set; }
            public string CalendarColor { get; set; }
            public string PhotostudioName { get; set; }
            public string PhotostudioImageUrl { get; set; }
            public string Color { get; set; }
            public IList<HallPropertyViewModel.Summary> Property { get; set; }

        }

        public class Descriptor : UniqueEntity
        {
            public Guid PhotostudioID { get; set; }
            public int Square { get; set; }
            public string Name { get; set; }
            public double MinimumPrice { get; set; }
            public double TodayPrice { get; set; }
            public string Description { get; set; }
            public string HallType { get; set; }
        }
    }

    public class HallViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Hall, HallViewModel.Details>()
                .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Name))
                .ForMember(dest => dest.Color, source => source.MapFrom(mem => mem.Calendar.Color))
                .ForMember(dest => dest.Description, source => source.MapFrom(mem => mem.Description))
                .ForMember(dest => dest.CalendarID, source => source.MapFrom(mem => mem.Calendar.ID))
                .ForMember(dest => dest.Square, source => source.MapFrom(mem => mem.Square))
                .ForMember(dest => dest.MinimumPrice, source => source.MapFrom(mem => mem.Schedule.Min(x => x.Price)))
                .ForMember(dest => dest.TodayPrice, source => source.MapFrom(mem => mem.GetDaylyPrice(DateTime.Now)))
                .ForMember(dest => dest.PhotostudioName, source => source.MapFrom(mem => mem.Photostudio.Name))
                .ForMember(dest => dest.PhotostudioImageUrl, source => source.MapFrom(mem => mem.Photostudio.ImageUrl))
                .ForMember(dest => dest.Photostudio, source => source.Ignore());

            CreateMap<Hall, HallViewModel.Descriptor>()
                .ForMember(x=>x.HallType, y=>y.MapFrom(z=>z.HallType))
                .ForMember(x => x.MinimumPrice, dest => dest.MapFrom(z => z.Schedule.Min(q => q.Price)))
                .ForMember(x => x.TodayPrice, dest => dest.MapFrom(z => z.GetDaylyPrice(DateTime.Now)));

            CreateMap<Hall, HallViewModel.Summary>()
                .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Name))
                .ForMember(dest => dest.Color, source => source.MapFrom(mem => mem.Calendar.Color))
                .ForMember(dest => dest.Description, source => source.MapFrom(mem => mem.Description))
                .ForMember(dest => dest.CalendarID, source => source.MapFrom(mem => mem.Calendar.ID))
                .ForMember(dest => dest.Square, source => source.MapFrom(mem => mem.Square))
                .ForMember(dest => dest.MinimumPrice, source => source.MapFrom(mem => mem.Schedule.Min(x => x.Price)))
                .ForMember(dest => dest.TodayPrice, source => source.MapFrom(mem => mem.GetDaylyPrice(DateTime.Now)))
                .ForMember(dest => dest.PhotostudioName, source => source.MapFrom(mem => mem.Photostudio.Name))
                .ForMember(dest => dest.PhotostudioImageUrl, source => source.MapFrom(mem => mem.Photostudio.ImageUrl));
        }
    }

}