using System;
using System.Collections.Generic;
using AutoMapper;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Models.EditViewModel.Interfaces;


namespace PhotoMSK.Models.EditViewModel.Routes
{
    public class MasterclassEditModel : RouteEditModel
    {
        public string Title { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int? Days { get; set; }
        public string Listeners { get; set; }
        public string AuthorName { get; set; }
  
    }
    public class MasterclassEditProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Masterclass, MasterclassEditModel>();
        }
    }
}