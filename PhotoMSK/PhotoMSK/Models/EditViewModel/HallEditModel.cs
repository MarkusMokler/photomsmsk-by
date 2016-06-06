using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.Models.Attributes;

namespace PhotoMSK.Models.EditViewModel
{
    public class HallEditModel : AbstractEditModel
    {
        public Guid? PhotostudioID { get; set; }
       
        [RequiredPost]
        [RegularExpression("[А-яA-z 0-9,.-]{3,25}$")]
        public string Name { get; set; }
        public decimal? Square { get; set; }
        public string Description { get; set; }
        public string TeaserImage { get; set; }
        public string TeaserImageUrl { get; set; }
    }

    public class HallEditModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Hall, HallEditModel>();
        }
    }
}