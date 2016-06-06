using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Models.Attributes;
using PhotoMSK.Models.EditViewModel.Interfaces;

namespace PhotoMSK.Models.EditViewModel.Routes
{
    public class PhotorentEditModel : RouteEditModel
    {
        [RequiredPost]
        [RegularExpression("[А-яA-z 0-9]{3,25}$")]
        public string Name { get; set; }
        public String City { get; set; }
        //public String MetroStation { get; set; }

         /* public double? Latitude { get; set; }
          public double? Longitude { get; set; }
          public string  Address { get; set; }
          public string  LegalAdress { get; set; } //юридический адрес
          public string  YNP { get; set; } //УНП
         * 
          public string  TradeRegister { get; set; } //ЗАРЕГИСТРИРОВАН В ТОРГОВОМ РЕЕСТРЕ 
          public string  СertificateNumber { get; set; } //НОМЕР СВИДЕТЕЛЬСТВА О РЕГИСТРАЦИИ 
          public DateTime DateRegister { get; set; } //ВЫДАНО СВИДЕТЕЛЬСТВО О РЕГИСТРАЦИИ(дата)
          public string  RegisteringAgency { get; set; }*/
    }
    public class PhotorentEditModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Photorent, PhotorentEditModel>();
        }
    }
}