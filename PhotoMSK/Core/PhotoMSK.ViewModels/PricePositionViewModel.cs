using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Routes;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public class PricePositionViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public virtual PhotoshopViewModel.Summary Photoshop { get; set; }
    }

    public class PricePositionViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<PricePosition, PricePositionViewModel>()
                .ForMember(dest => dest.ID, source => source.MapFrom(mem => mem.Phototechnics.ID))
                .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Phototechnics.Name))
                .ForMember(dest => dest.ShortDescription, source => source.MapFrom(mem => mem.Phototechnics.Description))
                .ForMember(dest => dest.Price, source => source.MapFrom(mem => mem.Price))
                .ForMember(dest => dest.Image, source => source.MapFrom(mem => mem.Phototechnics.ImageUrl));

        }
    }
}