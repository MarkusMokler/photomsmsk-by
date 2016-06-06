using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Extras.Quartz;
using Autofac.Integration.Mvc;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using AutoMapper;
using Fotobel.Classes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using PhotoMSK.Data;
using PhotoMSK.ViewModels.Autofac;
using PhotoMSK.ViewModels.Interfaces;
using Vk.SDK.Context;
using Vk.SDK.Http;
using IWallService = Vk.SDK.Interfaces.IWallService;


namespace Fotobel
{
    public static class AutofacConfig
    {
        /// <summary>
        /// Starts the application
        /// </summary>
        public static IContainer Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(Startup).Assembly);
            builder.RegisterApiControllers(typeof(Startup).Assembly);
            builder.RegisterHubs(typeof(Startup).Assembly);

            RegisterServices(builder);
            var container = builder.Build();

            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;

            // Set the dependency resolver for MVC.
            //var mvcResolver = new AutofacDependencyResolver(container);
            //DependencyResolver.SetResolver(mvcResolver);


            GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);
            return container;

        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<LuceneProvider>().As<ILuceneProvider>().SingleInstance();
            builder.RegisterType<UserProvider>().As<IUserIdProvider>();
            builder.RegisterType<UserInformationProvider>().As<IUserInformationProvider>();

            builder.RegisterType<UrlBuilder>().As<IUrlBuilder>();


            builder.RegisterType<RequestCreator>().As<IRequestCreator>();
            builder.RegisterType<RequestFactory>().As<IRequestFactory>();
            //    kernel.Bind<IUsersService>().To<UsersService>();
            builder.RegisterType<WallService>().As<IWallService>();
            builder.Register(c => Mapper.Engine).As<IMappingEngine>().SingleInstance();

            // 1) Register IScheduler
            builder.RegisterModule(new QuartzAutofacFactoryModule());
            // 2) Register jobs
            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(AutofacConfig).Assembly));

            AutofacRegistrator.RegisterTypes(builder);
        }
    }

    internal class UserInformationProvider : IUserInformationProvider
    {
        public bool IsAuthentificated => HttpContext.Current.User.Identity.IsAuthenticated;

        public Guid GetUserInformationID()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return Guid.Empty;

            var id = HttpContext.Current.User.Identity.GetUserId();

            using (AppContext context = new AppContext())
            {
                return context.UserInformations.Single(x => x.User.Id == id).ID;
            }

        }
    }

    internal class RequestCreator : IRequestCreator
    {
        public WebRequest Create(string url)
        {
            return WebRequest.Create(url);
        }
    }
}