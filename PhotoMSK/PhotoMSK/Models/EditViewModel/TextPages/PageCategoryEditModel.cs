using System;
using PhotoMSK.Data.Models;


namespace PhotoMSK.Models.EditViewModel.TextPages
{
    public class PageCategoryEditModel : AbstractEditModel
    {
        public Guid RouteID { get; set; }
        public string Slug { get; set; }
        public Guid? ID { get; set; }
        public string CategoryName { get; set; }
    }

}

namespace PhotoMSK.Models.ViewModels.TextPages
{
    public class PageCategoryViewModel
    {
        public Guid RouteID { get; set; }
        public string Slug { get; set; }
        public Guid? ID { get; set; }
        public string CategoryName { get; set; }
    }

    public class PageCategoryEditModelProfile : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<PageCategory, PageCategoryViewModel>();
        }
    }
}
