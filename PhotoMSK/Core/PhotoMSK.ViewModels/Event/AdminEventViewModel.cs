using System;
using System.Linq;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Event
{
    public class AdminEventViewModel : IEventViewModel
    {
        #region IEventViewModel members

        public Guid ID { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string ClassName { get; set; }

        public bool Editable => DateTime.Now < Start;

        public bool StartEditable => DateTime.Now < Start;

        public bool DurationEditable => DateTime.Now < Start;

        public string Color { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string TextColor { get; set; }

        #endregion

        public string Description { get; set; }
        public string Tags { get; set; }
        public bool AllDay { get; set; }
        public DateTime CreaTime { get; set; }
        public String Url { get; set; }
        public double Price { get; set; }
        public Guid UserID { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserUserPhoto { get; set; }
        public string Phone { get; set; }
        public UserInformationViewModel.Summary CreateBy { get; set; }
        public string EventState { get; set; }
        public double PrePayed { get; set; }
        public string UserClientType { get; set; }

   //     public HallViewModel.Descriptor Hall { get; set; }
    }

    public class AdminEventViewModelProfile : MapperProfile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Data.Models.Event, AdminEventViewModel>()
                .ForMember(x => x.Phone,
                    y => y.MapFrom(z => string.Join(",", z.User.Phones.Select(xx => xx.Phone.Number))))
                    .ForMember(x => x.PrePayed, y => y.MapFrom(x => x.PrePayed))
                .ForMember(x => x.StartEditable, y => y.Ignore())
                .ForMember(x => x.Editable, y => y.Ignore())
                .ForMember(x => x.DurationEditable, y => y.Ignore())
                .ForMember(x => x.CreateBy, opt => opt.MapFrom(x => x.CreatedBy))
                .ForMember(x => x.EventState, y => y.MapFrom(x => x.EventState.ToString()));

             //   .ForMember(x => x.Hall, x => x.MapFrom(y => y.Calendar.RouteEntity.As<Photostudio>().Halls.Single(a => a.Calendar.ID.Equals(y.Calendar.ID)).As<HallViewModel.Descriptor>()));
        }
    }
}