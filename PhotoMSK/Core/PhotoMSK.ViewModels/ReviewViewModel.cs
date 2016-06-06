using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class ReviewViewModel
    {
        public class Summary
        {
            public virtual string User_Id { get; set; }
            public bool Like { get; set; }
        }

        public class Details
        {
            public virtual UserViewModel.Summary User { get; set; }
            public virtual string User_Id { get; set; }
            public bool Like { get; set; }
        }
    }

    public class ReviewViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Review, ReviewViewModel.Summary>();
            CreateMap<Review, ReviewViewModel.Details>();
        }
    }
}
