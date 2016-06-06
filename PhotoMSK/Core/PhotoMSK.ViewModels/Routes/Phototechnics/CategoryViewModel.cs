using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Route
{
    public class CategoryViewModel
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }
        public string Slug { get; set; }

        public Guid ID { get; set; }

        public List<BrandViewModel> Brands { get; set; }
    }

    public class CategoryViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Category, CategoryViewModel>()
                 .ForMember(dest => dest.ID, source => source.MapFrom(mem => mem.ID))
                 .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Name))
                 .ForMember(dest => dest.Slug, source => source.MapFrom(mem => mem.Slug))
                 .ForMember(dest => dest.Brands, source => source.MapFrom(mem => mem.Brands.Select(x => x.Brand)));

            CreateMap<ShopCategory, CategoryViewModel>()
               .ForMember(dest => dest.ID, source => source.MapFrom(mem => mem.ID))
               .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Name))
               .ForMember(dest => dest.Slug, source => source.MapFrom(mem => mem.Slug))
               .ForMember(dest => dest.Brands, source => source.MapFrom(mem => mem.Brands.Select(x => x.Brand)));

        }
    }
}