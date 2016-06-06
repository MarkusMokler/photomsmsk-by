using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static partial class LikeStatusViewModel
    {
        public class Summary
        {
            public int Likes { get; protected set; }
            public int Dislikes { get; protected set; }
        }
    }

    public class LikeStatusViewModelSummaryProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<LikeStatus, LikeStatusViewModel.Summary>();
            CreateMap<LikeStatus, LikeStatusViewModel.Details>();
        }
    }
}
