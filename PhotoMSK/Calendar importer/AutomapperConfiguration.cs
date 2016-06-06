using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Comments;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Social;

namespace Calendar_importer
{
    public static class AutomapperConfiguration
    {

        public static void Configure(IContainer container)
        {
            FromAssembly(container);

            //            Mapper.CreateMap<UserInformation, RegisterViewModel>();
            //          Mapper.CreateMap<UserInformation, ResetPasswordModel>();


            Mapper.CreateMap<Penalty, PenaltyViewModel.Details>();
            Mapper.CreateMap<Penalty, PenaltyViewModel.Summary>()
                .ForMember(x => x.Description, y => y.MapFrom(z => z.Description))
                .ForMember(x => x.Date, y => y.MapFrom(z => z.Event.Start))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price))
                .ForMember(x => x.Route, y => y.MapFrom(z => z.Event.Calendar.RouteEntity));


            Mapper.CreateMap<RouteEntity, RoutePreviewViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.ToString()))
                .ForMember(x => x.Shortcut, y => y.MapFrom(z => z.Shortcut))
                .ForMember(x => x.Photo, y => y.MapFrom(z => z.TeaserImage));

            Mapper.CreateMap<IEnumerable<WallPost>, MainWallViewModel>()
                .ForMember(x => x.Items, y => y.MapFrom(z => z.ToList()));

            Mapper.CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.IsAnswer, y => y.MapFrom(z => z.IsAnswer))
                .ForMember(x => x.AnsweredName, y => y.MapFrom(z => z.IsAnswer == true ? z.AnsweredUser.FirstName : ""))
                .ForMember(x => x.CreationTime, y => y.MapFrom(z => z.CreationTime))
                .ForMember(x => x.ID, y => y.MapFrom(z => z.ID))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.User.FirstName + " " + z.User.LastName))
                .ForMember(x => x.Photo, y => y.MapFrom(z => z.User.UserPhoto))
                .ForMember(x => x.Text, y => y.MapFrom(z => z.Text))
                .ForMember(x => x.UserInformationID, y => y.MapFrom(z => z.User.ID));

            Mapper.CreateMap<UserInformation, UserInformationViewModel.Summary>();

            Mapper.CreateMap<String, PhoneViewModel.Summary>()
                .ForMember(x => x.Number, y => y.MapFrom(z => z));

            Mapper.CreateMap<Photostudio, PhotostudioViewModel.Detaills>()
                .ForMember(x => x.Administrators, y => y.Ignore())
                .ForMember(x => x.Creator, y => y.Ignore());

            Mapper.CreateMap<Photomodel, PhotomodelViewModel.Summary>()
                .ForMember(x => x.Phones, y => y.MapFrom(z => z.GetPhones()));
        }


        public static void FromAssembly(IContainer container)
        {
            Mapper.Initialize(x =>
            {
                x.ConstructServicesUsing(container.Resolve);
            });

            GetConfiguration(Mapper.Configuration);

        }

        private static void GetConfiguration(IConfiguration configuration)
        {
            var profiles = new List<Type>();
            //var profiles = typeof(AutomapperConfiguration).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x)).ToList();

            profiles.AddRange(typeof(WallPostViewModel).Assembly.GetTypes().Where(x => typeof(MapperProfile).IsAssignableFrom(x) && x.IsAbstract == false));

            foreach (var aprofile in profiles.Select(profile => Activator.CreateInstance(profile) as MapperProfile))
            {
                aprofile.Execute(configuration);
            }
        }

    }
}