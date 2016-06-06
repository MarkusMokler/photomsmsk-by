using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class SmsMessageViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public string Phone { get; set; }
            public string Message { get; set; }
        }

        public class Details
        {
            public Guid ID { get; set; }
            public string Phone { get; set; }
            public string Message { get; set; }
        }
    }

    public class SmsMessageViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<SmsMessage, SmsMessageViewModel.Summary>();
            CreateMap<SmsMessage, SmsMessageViewModel.Details>();
        }
    }
}
