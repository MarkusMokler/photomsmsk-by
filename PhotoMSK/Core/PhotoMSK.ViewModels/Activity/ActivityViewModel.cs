using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models.ActivityStream;
using PhotoMSK.Data.Models.ShoppingCart;
using PhotoMSK.ViewModels.Advertising;
using PhotoMSK.ViewModels.Event;
using PhotoMSK.ViewModels.Routes.Photostudio;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Activity
{
    public static class ActivityViewModel
    {
        public abstract class Summary
        {
            public Guid ID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime ActivityTime { get; set; }
            public Guid RouteID { get; set; }
            public Guid UserID { get; set; }
            public virtual UserInformationViewModel.Summary User { get; set; }

        }
    }


    public static class EventActivityViewModel
    {
        public class Summary : ActivityViewModel.Summary
        {
            public virtual AdminEventViewModel Event { get; set; }
            public Guid EventID { get; set; }
            public EventActivityState State { get; set; }
        }
    }


    public static class CallActivityViewModel
    {
        public class Summary : ActivityViewModel.Summary
        {
            public virtual EventViewModel Event { get; set; }
            public Guid EventID { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public CallType CallType { get; set; }
            public string VoiceRecord { get; set; }
        }

    }

    public class ActivityViewModelViewModelProfile : MapperProfile
    {
        protected override void Configure()
        {

            CreateMap<Data.Models.ActivityStream.Activity, ActivityViewModel.Summary>()
                .ForMember(x => x.User, opt => opt.MapFrom(source => source.User));

            CreateMap<EventActivity, EventActivityViewModel.Summary>()
                .IncludeBase<Data.Models.ActivityStream.Activity, ActivityViewModel.Summary>()
                .ForMember(dest => dest.Event, act => act.MapFrom(source => source.Event));

            CreateMap<CallActivity, CallActivityViewModel.Summary>()
               .IncludeBase<Data.Models.ActivityStream.Activity, ActivityViewModel.Summary>()
               .ForMember(dest => dest.Event, act => act.MapFrom(source => source.Event));
        }
    }

}
