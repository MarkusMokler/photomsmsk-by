using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Routes
{
    public static class SmsModuleViewModel
    {
        public class Summary : UniqueEntity
        {
            public bool SmsEnnabled { get; set; }
            public string BookingLineText { get; set; }
            public double SmsBalance { get; set; }
            public string SmsSender { get; set; }
            public string BookingHelloText { get; set; }
            public string BookingEndText { get; set; }

        }
    }
    public class SmsModuleViewModelProfile : MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<SmsModule, SmsModuleViewModel.Summary>();
        }
    }
}
