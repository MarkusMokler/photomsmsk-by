using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PhotoMSK.Data.Models;


namespace PhotoMSK.Models.EditViewModel
{
    public class TextPageEditModel : AbstractEditModel
    {
        public Guid? ID { get; set; }
        public Guid? RouteID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public bool? Published { get; set; }
        public string Slug { get; set; }
        public bool Comment { get; set; }
        public Guid PageCategoryID { get; set; }
    }

    public class TextPageEditModelProfile : Profile
    {

        protected override void Configure()
        {
            CreateMap<TextPage, TextPageEditModel>();
        }
    }

}