using System;
using System.Collections.Generic;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Event
{
    public class SaveEventViewModel
    {
        public bool Admin { get; set; }
        public string GoogleID { get; set; }
        public string UserPhone { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public bool Accept { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool AllDay { get; set; }
        public Guid ID { get; set; }
        public string Tags { get; set; }
        public Guid CalendarID { get; set; }
        public EventState EventState { get; set; }
        public double PrePayed { get; set; }
    }

    public class SaveEventViewModelProfile : MapperProfile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Data.Models.Event, SaveEventViewModel>();
        }
    }
}