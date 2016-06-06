using System;
using System.Linq;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Routes.Photostudio
{
    public class EventViewModel
    {
        public Guid ID;
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public bool AllDay { get; set; }
        public Guid CalendarID { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime CreaTime { get; set; }
        public string Url { get; set; }
        public double Price { get; set; }

        public string ClassName { get; set; }

        public bool Editable => DateTime.Now < Start;
        public bool StartEditable => DateTime.Now < Start;

        public bool DurationEditable
        {
            get { return DateTime.Now < Start; }
        }
        public string Color { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string TextColor { get; set; }
        public string UserFirstName { get; set; }
        public string UserID { get; set; }
        public string UserLastName { get; set; }
        public string UserUserPhoto { get; set; }

        public string UserClientType { get; set; }
        public string Phone { get; set; }
    }

    public class EventViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Data.Models.Event, EventViewModel>()
                .ForMember(x => x.UserFirstName, y => y.MapFrom(z => z.User.FirstName))
                .ForMember(x => x.UserLastName, y => y.MapFrom(z => z.User.LastName))
                .ForMember(x => x.UserUserPhoto, y => y.MapFrom(z => z.User.UserPhoto))
                .ForMember(x => x.UserClientType, y => y.MapFrom(z => z.User.ClientType))
                .ForMember(x => x.Phone, y => y.MapFrom(z => z.User.Phones.FirstOrDefault().Phone.Number));
        }
    }
}
