using System;
using System.Linq;
using AutoMapper;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Event
{
    public class UserEventViewModel : IEventViewModel
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string ClassName { get; set; }

        public string Tags { get; set; }
        public bool Editable => false;

        public bool StartEditable => false;

        public bool DurationEditable => false;

        public string Color { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string TextColor { get; set; }
        public string EventState { get; set; }
    }

    public class UserEventViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Data.Models.Event, UserEventViewModel>()
                .ForMember(x => x.Tags, x => x.MapFrom(y => y.Tags.Contains("online") ? "онлайн" : ""));
        }
    }
}