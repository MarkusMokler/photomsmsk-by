using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;

namespace PhotoMSK.Models.EditViewModel
{
    public class UserInformationEditModel : AbstractEditModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserPhoto { get; set; }
        public AccessStatus AccessStatus { get; set; }
    }

    public class UserInformationEditModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Role, UserInformationEditModel>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(z => z.UserInformation.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(z => z.UserInformation.LastName))
                .ForMember(x => x.UserPhoto, opt => opt.MapFrom(z => z.UserInformation.UserPhoto))
                .ForMember(x => x.AccessStatus, opt => opt.MapFrom(z => z.AccessStatus));
            
            base.Configure();
        }
    }
}