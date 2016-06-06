using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Routes
{
    public class RouteReviewViewModel
    {
        public class Summary : RouteReviewViewModel
        {
            public Guid ID { get; set; }
            public UserInformationViewModel.Client User { get; set; }
            public LikeStatusViewModel.Summary LikeStatus { get; set; }
            public DateTime Date { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string GoodComment { get; set; }
            public string BadComment { get; set; }
        }

        public class RouteReviewProfile : MapperProfile
        {
            protected override void Configure()
            {
                CreateMap<RouteReview, Summary>();
            }
        }
    }
}
