using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Attachments;
using PhotoMSK.ViewModels.Comments;

namespace PhotoMSK.ViewModels.Social
{
    public static class WallPostViewModel
    {
        public class Summary : UniqueEntity
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime Date { get; set; }
            public int Views { get; set; }
        }

        public class Details : Summary
        {
            public IEnumerable<Attachments.VideoViewModel.Summary> Videos => Attacments?.OfType<Attachments.VideoViewModel.Summary>();

            public IEnumerable<Attachments.PhotoViewModel.Summary> Photos => Attacments?.OfType<Attachments.PhotoViewModel.Summary>();
            public virtual ICollection<RoutePreviewViewModel> Participants { get; set; }
            public virtual ICollection<AttachmentViewModel.Summary> Attacments { get; set; }
            public virtual LikeStatusViewModel.Summary LikeStatus { get; set; }
            public virtual CommentListViewModel.Summary Comments { get; set; }
            public string Name { get; set; }
            public int Likes { get; set; }
            public int Dislikes { get; set; }

            public string AuthorImage { get; set; }
            public string AuthorShortcut { get; set; }
            public string AuthorUrl { get; set; }

        }
    }

    public class WallPostViewModelProfile : MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<WallPost, WallPostViewModel.Summary>();
            CreateMap<WallPost, WallPostViewModel.Details>()
                .ForMember(x => x.Description, y => y.MapFrom(z => ConvertUrlsToLinks(z.Desctiption)))
                .ForMember(x => x.Attacments, y => y.Ignore())
                .ForMember(x => x.Participants, y => y.Ignore())
                .ForMember(x => x.Comments, y => y.MapFrom(z => z.Comments));
            CreateMap<WallPostViewModel.Details, WallPostViewModel.Details>();

        }
        public static string ConvertUrlsToLinks(string msg)
        {
            string regex = @"((www\.|(http|https|ftp|news|file)+\:\/\/)[&#95;.a-z0-9-]+\.[a-z0-9_\/&#95;:@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\)])";
            Regex r = new Regex(regex, RegexOptions.IgnoreCase);
            return r.Replace(msg, "<a href=\"$1\" title=\"Click to open in a new window or tab\" target=\"&#95;blank\">$1</a>").Replace("href=\"www", "href=\"http://www");
        }
    }

    public abstract class MapperProfile
    {
        private IConfiguration _cfg;
        public void Execute(IConfiguration cfg)
        {
            _cfg = cfg;
            Configure();
        }

        protected abstract void Configure();

        public IMappingExpression<TS, TD> CreateMap<TS, TD>()
        {
            return _cfg.CreateMap<TS, TD>();
        }
    }
}
