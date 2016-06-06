using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Clients;
using PhotoMSK.ViewModels.Routes;
using PhotoMSK.ViewModels.Routes.Photostudio;
using PhotoMSK.ViewModels.ShoppingCart;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class UserInformationViewModel
    {
        public class Summary : UniqueEntity
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserPhoto { get; set; }
            public string UserPhone { get; set; }
            public bool IsCup { get; set; }
            public bool Agreement { get; set; }
            public string VkLink { get; set; }
            public string FacebookLink { get; set; }
            public string Instagram { get; set; }
            public string Googleplus { get; set; }
            public string Site { get; set; }
            public string ClientType { get; set; }
            public string Email { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public DateTime? DateOfBirth { get; set; }
        }

        public class Client : Summary
        {
            public double Total { get; set; }
            public IList<TagViewModel> Tags { get; set; }
            public double TotalDiscarded { get; set; }
            public decimal TotalPenaltied { get; set; }
            public int Bookings { get; set; }
            public int Movings { get; set; }
            public int Discards { get; set; }
        }
        public class TagViewModel
        {
            public Guid ID { get; set; }
            public string Text { get; set; }
        }
        public class Details : Summary
        {
            public virtual ICollection<UserPhoneViewModel.Summary> Phones { get; set; }
            public virtual ICollection<PenaltyViewModel.Summary> Penalties { get; set; }
            public virtual ICollection<EventViewModel> Events { get; set; }
            public virtual ICollection<RoleViewModel.Summary> Roles { get; set; }
            public virtual ICollection<SaleCardViewModel.Summary> Cards { get; set; }
            public virtual ICollection<ShippingAddressViewModel.Summary> Adresses { get; set; }
            public ICollection<OrderViewModel.Summary> Orders { get; set; }
        }

        public class Participant : Summary
        {
            public AccessStatus AccessStatus { get; set; }

        }

    }
    public class UserInformationExtendedViewModelProfile : MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Tag, UserInformationViewModel.TagViewModel>()
                .ForMember(x => x.Text, opt => opt.MapFrom(dest => dest.Name));
            CreateMap<UserInformation, UserInformationViewModel.Summary>()
                .ForMember(x => x.UserPhone, opt => opt.MapFrom(z => z.Phones.First().Phone.Number));

            CreateMap<UserInformation, UserInformationViewModel.Client>()
              .ForMember(x => x.UserPhone, opt => opt.MapFrom(z => z.Phones.First().Phone.Number));

            CreateMap<UserInformation, UserInformationViewModel.Participant>()
                .ForMember(x => x.UserPhone, opt => opt.MapFrom(z => z.Phones.First().Phone.Number))
                .ForMember(x => x.ID, y => y.MapFrom(z => z.ID))
                .ForMember(x => x.AccessStatus, opt => opt.UseValue(AccessStatus.None));


            CreateMap<Role, UserInformationViewModel.Participant>()
                .ForMember(x => x.UserPhone, opt => opt.MapFrom(z => z.UserInformation.Phones.First().Phone.Number))
                .ForMember(x => x.ID, opt => opt.MapFrom(z => z.ID))
                .ForMember(x => x.AccessStatus, opt => opt.MapFrom(z => z.AccessStatus))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(z => z.UserInformation.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(z => z.UserInformation.LastName))
                .ForMember(x => x.UserPhoto, opt => opt.MapFrom(z => z.UserInformation.UserPhoto));


            CreateMap<UserInformation, UserInformationViewModel.Details>()
                    .ForMember(x => x.UserPhone, opt => opt.MapFrom(z => z.Phones.First().Phone.Number))
                    .ForMember(x => x.Events, y => y.MapFrom(z => z.Events));
        }
    }

}