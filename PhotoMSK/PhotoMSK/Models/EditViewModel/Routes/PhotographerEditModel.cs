using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Models.Attributes;
using PhotoMSK.Models.EditViewModel.Interfaces;
using PhotoMSK.ViewModels;

namespace PhotoMSK.Models.EditViewModel.Routes
{
    public class PhotographerEditModel : RouteEditModel, IFioShortcutDescription
    {
        [RequiredPost]
        [RegularExpression("[А-яA-z]{3,15}$")]
        public string FirstName { get; set; }

        [RequiredPost]
        [RegularExpression("[А-яA-z]{3,15}$")]
        public string LastName { get; set; }

        public string City { get; set; }
        public IEnumerable<PhoneViewModel.Summary> Phones { get; set; }
    }

    public class PhotographerEditModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Photographer, PhotographerEditModel>()
                .ForMember(x => x.Phones, dest => dest.MapFrom(z => z.Phones));
        }
    }
}