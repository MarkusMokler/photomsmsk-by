using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Comments;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Comments
{
    public static class CommentListViewModel
    {
        public class Summary:UniqueEntity
        {
            public int CommentsCount { get; set; }
            public virtual ICollection<CommentViewModel.Summary> Comments { get; set; }
        }

        public class Details : Summary
        {
         

        }
    }

    public class CommentListViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<CommentList, CommentListViewModel.Summary>()  
                .ForMember(dest => dest.Comments,
                    opt => opt.MapFrom(
                        src => src.Comments
                            .OrderByDescending(x => x.CreationTime)
                            .Take(3)
                            .ToList()));
      
            CreateMap<CommentList, CommentListViewModel.Details>();

        }
    }
}
