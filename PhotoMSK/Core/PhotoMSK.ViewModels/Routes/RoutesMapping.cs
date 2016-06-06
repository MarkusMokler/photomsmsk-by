using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Automapper;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Social;
using Page = PhotoMSK.Data.Models.Routes.Page;

namespace PhotoMSK.ViewModels.Routes
{
    public class RoutesMapping : MapperProfile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with thisMapperProfile.
        ///             Avoid calling the <see cref="T:AutoMapper.Mapper"/> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<RouteEntity, RouteEntityViewModel.Summary>()
                .ForMember(x => x.Phones, dest => dest.MapFrom(o => o.Phones))
                .ForMember(x => x.Url, y => y.ResolveUsing<ShortcutResolver>());

            CreateMap<Page, PageViewModel.Summary>()
                .IncludeBase<RouteEntity, RouteEntityViewModel.Summary>();

            CreateMap<Data.Models.Routes.Photographer, PhotographerViewModel.Summary>()
                     .ForMember(x => x.Phones, dest => dest.Ignore())
                     .ForMember(x => x.Url, y => y.ResolveUsing<ShortcutResolver>());

            CreateMap<Photomodel, PhotomodelViewModel.Summary>()
                .IncludeBase<RouteEntity, RouteEntityViewModel.Summary>();

            CreateMap<Photorent, PhotorentViewModel.Summary>()
                .IncludeBase<RouteEntity, RouteEntityViewModel.Summary>();

            CreateMap<Photoshop, PhotoshopViewModel.Summary>()
                .IncludeBase<RouteEntity, RouteEntityViewModel.Summary>();

            CreateMap<Data.Models.Routes.Photostudio, PhotostudioViewModel.Summary>()
                .ForMember(x => x.Phones, dest => dest.Ignore())
                .ForMember(x => x.Halls, dest => dest.Ignore())
                .ForMember(x => x.HallsCount, dest => dest.MapFrom(z => z.HallCount))
                .ForMember(x => x.MinimumPrice, dest => dest.MapFrom(x => x.Halls != null && x.Halls.Count > 0 ? x.Halls.Min(y => y.GetDaylyPrice(DateTime.Now)) : 0))
                .ForMember(x => x.HallsSquare, dest => dest.MapFrom(x => x.Halls != null && x.Halls.Count > 0 ? x.Halls.Sum(y => y.Square) : 0))
                .ForMember(x => x.Url, y => y.ResolveUsing<ShortcutResolver>());

            CreateMap<Place, PlaceViewModel.Summary>()
                .IncludeBase<RouteEntity, RouteEntityViewModel.Summary>();

            CreateMap<RouteEntity, RouteEntityViewModel.Details>()
                .Include<Data.Models.Routes.Photographer, PhotographerViewModel.Details>()
                .ForMember(x => x.Wall, y => y.ResolveUsing<WallResolver>());

            CreateMap<Data.Models.Routes.Photographer, PhotographerViewModel.Details>()
                .Include<Data.Models.Routes.Photographer, PhotographerViewModel.Details>()
             .ForMember(dest => dest.FirstName, source => source.MapFrom(mem => mem.FirstName))
             .ForMember(dest => dest.LastName, source => source.MapFrom(mem => mem.LastName))
             .ForMember(dest => dest.City, source => source.MapFrom(mem => mem.City))
              .ForMember(dest => dest.Geners,
                 source => source.MapFrom(mem => mem.Geners.OrderBy(x => x.Level).Take(15).Select(z => z.Genre.Name)))
             .ForMember(dest => dest.Phones, source => source.MapFrom(mem => mem.Phones.Select(x => x.Phone.Number)));


            CreateMap<Page, PageViewModel.Details>()
                .IncludeBase<RouteEntity, RouteEntityViewModel.Details>();

            CreateMap<Photomodel, PhotomodelViewModel.Details>()
                .IncludeBase<RouteEntity, RouteEntityViewModel.Details>();

            CreateMap<Photorent, PhotorentViewModel.Details>()
                .IncludeBase<RouteEntity, RouteEntityViewModel.Details>()
                .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Name))
                .ForMember(dest => dest.Creator,
                    source =>
                        source.MapFrom(
                            mem => mem.Participants.Single(x => x.AccessStatus == AccessStatus.Owner).UserInformation))
                .ForMember(dest => dest.Administrators,
                    source =>
                        source.MapFrom(
                            mem =>
                                mem.Participants.Where(x => x.AccessStatus == AccessStatus.Administrator)
                                    .Select(x => x.UserInformation)))
                .ForMember(dest => dest.Verified, source => source.MapFrom(mem => mem.Verified))
                .ForMember(dest => dest.Phones, source => source.MapFrom(mem => mem.Phones.Select(x => x.Phone.Number)));

            CreateMap<Photoshop, PhotoshopViewModel.Details>()
                .IncludeBase<RouteEntity, RouteEntityViewModel.Details>()
                .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Name))
                .ForMember(dest => dest.Creator,
                    source =>
                        source.MapFrom(
                            mem => mem.Participants.Single(x => x.AccessStatus == AccessStatus.Owner).UserInformation))
                .ForMember(dest => dest.Administrators,
                    source =>
                        source.MapFrom(
                            mem =>
                                mem.Participants.Where(x => x.AccessStatus == AccessStatus.Administrator)
                                    .Select(x => x.UserInformation)));

            CreateMap<Photoshop, PhotoshopViewModel.WhiteLabel>()
               .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Name))
               .ForMember(dest => dest.Description, source => source.MapFrom(mem => mem.Description))
               .ForMember(dest => dest.ID, source => source.MapFrom(mem => mem.ID))
               .ForMember(dest => dest.Shortcut, source => source.MapFrom(mem => mem.Shortcut))
               .ForMember(dest => dest.Domain, source => source.MapFrom(mem => mem.Domain));

            CreateMap<Data.Models.Routes.Photostudio, PhotostudioViewModel.Detaills>()
                .IncludeBase<RouteEntity, RouteEntityViewModel.Details>()
                .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Name))
                .ForMember(dest => dest.Creator,
                    source =>
                        source.MapFrom(
                            mem => mem.Participants.Single(x => x.AccessStatus == AccessStatus.Owner).UserInformation))
                .ForMember(dest => dest.Administrators,
                    source =>
                        source.MapFrom(
                            mem =>
                                mem.Participants.Where(x => x.AccessStatus == AccessStatus.Administrator)
                                    .Select(x => x.UserInformation)))
                .ForMember(dest => dest.Halls,
                    source => source.MapFrom(photorent => photorent.Halls));

            CreateMap<Place, PlaceViewModel.Details>().IncludeBase<RouteEntity, RouteEntityViewModel.Details>();

            CreateMap<Page, PublicViewModel.Summary>()
                .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Name))
                .ForMember(dest => dest.Verified, source => source.MapFrom(mem => mem.Verified))
                .ForMember(dest => dest.Phones, source => source.MapFrom(mem => mem.Phones.Select(x => x.Phone)));

            CreateMap<Page, PublicViewModel.Details>().IncludeBase<RouteEntity, RouteEntityViewModel.Details>()
             .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Name))
             .ForMember(dest => dest.Creator,
                 source =>
                     source.MapFrom(
                         mem => mem.Participants.Single(x => x.AccessStatus == AccessStatus.Owner).UserInformation))
             .ForMember(dest => dest.Administrators,
                 source =>
                     source.MapFrom(
                         mem =>
                             mem.Participants.Where(x => x.AccessStatus == AccessStatus.Administrator)
                                 .Select(x => x.UserInformation)))
             .ForMember(dest => dest.Verified, source => source.MapFrom(mem => mem.Verified))
             .ForMember(dest => dest.Phones, source => source.MapFrom(mem => mem.Phones.Select(x => x.Phone)));
        }
    }
}
