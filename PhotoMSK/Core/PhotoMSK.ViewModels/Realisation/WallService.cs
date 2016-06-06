using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.ViewModels.Attachments;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Realisation
{
    class WallService : AbstractService, IDisposable, IWallService, IWall
    {
        private readonly Guid _routeID;
        private readonly IUrlBuilder _urlBuilder;

        public WallService(IUrlBuilder urlBuilder)
        {
            _urlBuilder = urlBuilder;
        }
        public WallService(Guid routeID, IUrlBuilder urlBuilder)
        {
            _routeID = routeID;
            _urlBuilder = urlBuilder;
        }

        public IEnumerable<WallPostViewModel.Details> GetWallForRoute(Guid routeID)
        {
            var posts = Context.Viewses.Where(x => x.EntityID == routeID).OrderBy(x => x.Date).Take(10).Select(x => x.WallPost).Include(x => x.Referers).ToList();

            var listpost = Mapper.Map<IList<WallPostViewModel.Details>>(posts).ToList();

            LoadReferers(posts, listpost);
            LoadAttachments(listpost);

            listpost.ForEach(x => x.AuthorShortcut = _urlBuilder.GetShortcutUrl(x.AuthorShortcut));

            return listpost;
        }
        public IEnumerable<MainWallViewModel> GetMainWall(Guid? userID = null)
        {
            DateTime sdate = DateTime.Now.AddDays(-3);

            IList<IEnumerable<WallPost>> news =
                 Context.Newses
                 .Include(x => x.Referers)
                 .Where(x => x.Date > sdate)
                 .GroupBy(x => x.Date.Day)
                 .Select(x => x
                     .OrderByDescending(y => y.LikeStatus.Likes)
                     .ThenBy(z => Guid.NewGuid())
                     .Take(3))
                 .ToList();

            var board = Mapper.Map<IList<IList<WallPostViewModel.Details>>>(news);

            IList<WallPost> posts = news.SelectMany(@new => @new).ToList();
            var listpost = board.SelectMany(x => x).ToList();


            LoadReferers(posts, listpost);
            LoadAttachments(listpost);

            listpost.ForEach(x => x.AuthorShortcut = _urlBuilder.GetShortcutUrl(x.AuthorShortcut));


            var ids = news.SelectMany(x => x.Select(y => y.ID)).ToList();
            Context.WallPostAddView(ids);

            return board.Select(item => new MainWallViewModel { Items = item });
        }
        public WallPostViewModel.Details GetDetails(Guid id)
        {
            var posts = Context.Newses.Where(x => x.ID == id).ToList();

            var listpost = Mapper.Map<IList<WallPostViewModel.Details>>(posts).ToList();

            LoadReferers(posts, listpost);
            LoadAttachments(listpost);

            listpost.ForEach(x => x.AuthorShortcut = _urlBuilder.GetShortcutUrl(x.AuthorShortcut));

            return listpost.First();
        }

        private void LoadReferers(IList<WallPost> model, IList<WallPostViewModel.Details> detailses)
        {
            var elems = model.Select(x => x.ID).ToList();
            var views = Context.Viewses.Where(x => elems.Contains(x.WallPost.ID)).Include(x => x.Entity).ToList();


            foreach (var viewse in views)
            {
                var ppost = detailses.Single(x => x.ID == viewse.WallPost.ID);

                if (viewse.ParticipationType == ParticipationType.Owner)
                {
                    ppost.AuthorShortcut = viewse.Entity.Shortcut;
                    ppost.AuthorImage = viewse.Entity.ImageUrl;
                    ppost.AuthorUrl = _urlBuilder.GetShortcutUrl(viewse.Entity.Shortcut);
                    ppost.Name = viewse.Entity.GetName();
                }

                if (viewse.ParticipationType == ParticipationType.СoAuthor)
                {
                    if (ppost.Participants == null)
                        ppost.Participants = new List<RoutePreviewViewModel>();

                    ppost.Participants.Add(Mapper.Map<RoutePreviewViewModel>(viewse.Entity));

                }
            }

        }
        private void LoadAttachments(IList<WallPostViewModel.Details> model)
        {
            var idlist = model.Select(x => x.ID).ToList();

            foreach (var detailse in model)
            {
                detailse.Attacments = new List<AttachmentViewModel.Summary>();

            }

            var attachments = Context.Attachments.Where(x => idlist.Contains(x.WallPostID.Value)).ToList();


            foreach (var attachment in attachments)
            {
                var ppost = model.Single(x => x.ID == attachment.WallPostID);

                if (attachment is Photo)
                    ppost.Attacments.Add(Mapper.Map<PhotoViewModel.Summary>(attachment));
                if (attachment is Video)
                    ppost.Attacments.Add(Mapper.Map<VideoViewModel.Summary>(attachment));
            }
        }
        public IEnumerator<WallPostViewModel.Details> GetEnumerator()
        {
            var list = GetWallForRoute(_routeID);
            return list.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
