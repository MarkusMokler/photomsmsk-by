using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Models.Attributes;
using PhotoMSK.Models.EditViewModel.Interfaces;
using PhotoMSK.ViewModels;


namespace PhotoMSK.Models.EditViewModel.Routes
{
    public class PageEditModel : RouteEditModel
    {
        [RequiredPost]
        [RegularExpression("[А-яA-z 0-9,.-]{3,25}$")]
        public string Name { get; set; }
        public string Gender { get; set; } //тип страницы
        public string City { get; set; }
        public IEnumerable<PhoneViewModel.Summary> Phones { get; set; }
    }

    public class PageEditModedlProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Page, PageEditModel>()
                .ForMember(x => x.Phones, dest => dest.MapFrom(z => z.Phones));
        }
    }
}