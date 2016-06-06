using AutoMapper;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Attachments
{
    public static class CreativeViewModel
    {
        public class Summary
        {

        }

        public class Details
        {
            public PhotoGenerViewModel.Summary Genre { get; set; }
            public PhotoViewModel.Summary Photo { get; set; }
            public virtual LikeStatusViewModel.Summary LikeStatus { get; set; }
        }
    }

    public class CreativeViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Creative, CreativeViewModel.Summary>();
            CreateMap<Creative, CreativeViewModel.Details>();
        }
    }
}
