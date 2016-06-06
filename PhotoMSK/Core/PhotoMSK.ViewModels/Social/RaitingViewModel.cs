using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class RaitingViewModel
    {
        public class Summary  : RouteEntityViewModel.Summary
        {
            public int Views { get; protected set; }
            public int Likes { get; protected set; }
            public int Dislikes { get; protected set; }
            public int WallPosts { get; protected set; }
            public int Comments { get; protected set; }
            public int Payed { get; protected set; }
            public double Total { get; protected set; }
            public override string RouteType => "Rating";
        }

        public class Details : RouteEntityViewModel.Details
        {
            public override string RouteType => "Rating";
            public virtual RouteEntityViewModel.Summary Entity { get; set; }
            public int Views { get; protected set; }
            public int Likes { get; protected set; }
            public int Dislikes { get; protected set; }
            public int WallPosts { get; protected set; }
            public int Comments { get; protected set; }
            public int Payed { get; protected set; }
            public double Total { get; protected set; }
        }
    }

    public class RaitingViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Raiting, RaitingViewModel.Summary>();
            CreateMap<Raiting, RaitingViewModel.Details>();
        }
    }
}
