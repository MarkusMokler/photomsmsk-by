using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Comments;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Comments
{
    public static class CommentViewModel
    {
        public class Summary:UniqueEntity
        {
            public string Text { get; set; }
            public DateTime CreationTime { get; set; }
            public Guid UserInformationID { get; set; }
            public Guid? AnsweredUserInformationID { get; set; }
            public virtual Guid CommentsID { get; set; }
            public bool IsAnswer => AnsweredUserInformationID != null;
        }

        public class Details : Summary
        {
            public virtual UserInformation User { get; set; }
        }
    }

    public class CommentViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Comment, CommentViewModel.Summary>();
            CreateMap<Comment, CommentViewModel.Details>();
        }
    }
}
