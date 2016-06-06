using Autofac;
using PhotoMSK.ViewModels.Automapper;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.ViewModels.Autofac
{
    public class AutofacRegistrator
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<WallService>().As<IWallService>().UsingConstructor(typeof(IUrlBuilder)).OnRelease(x => x.Dispose());
            builder.RegisterType<UserInformationService>().As<IUserInformationService>().OnRelease(x => x.Dispose());

            builder.RegisterType<EventLocker>().As<IEventLocker>();

            builder.RegisterType<PhotostudioService>().As<IPhotostudioService>().OnRelease(x => x.Dispose());
            builder.RegisterType<PhotographerService>().As<IPhotographerService>().OnRelease(x => x.Dispose());
            builder.RegisterType<PhotomodelService>().As<IPhotomodelService>().OnRelease(x => x.Dispose());
            builder.RegisterType<PhotoshopService>().As<IPhotoshopService>().OnRelease(x => x.Dispose());
            builder.RegisterType<PhotorentService>().As<IPhotorentService>().OnRelease(x => x.Dispose());
            builder.RegisterType<MasterclassService>().As<IMasterclassService>().OnRelease(x => x.Dispose());
            builder.RegisterType<PageService>().As<IPageService>().OnRelease(x => x.Dispose());
            builder.RegisterType<PlaceService>().As<IPlaceService>().OnRelease(x => x.Dispose());
            builder.RegisterType<MenuService>().As<IMenuService>().OnRelease(x => x.Dispose());

            builder.RegisterType<PhototechnicsService>().As<IPhototechnicsService>().OnRelease(x => x.Dispose());
            builder.RegisterType<SmsAssistent>().As<ISmsAssistent>();

            builder.RegisterType<WallResolver>();

            builder.RegisterType<PhototechnicsPrifile.PriceResolver>();
            builder.RegisterType<PhototechnicsPrifile.CalculatedPriceResolver>();


            builder.RegisterType<PhototechnicsPrifile.CalculatedHourlyPriceResolver>();
            builder.RegisterType<PhototechnicsPrifile.CalculatedHalfDayPriceResolver>();
            builder.RegisterType<PhototechnicsPrifile.CalculatedDaylyPriceResolver>();
            builder.RegisterType<PhototechnicsPrifile.CalculatedHollydayPriceResolver>();
            builder.RegisterType<PhototechnicsPrifile.CalculatedWeeklyPriceResolver>();
            builder.RegisterType<PhototechnicsPrifile.CalculatedMonthlyPriceResolver>();

            builder.RegisterType<ShortcutResolver>();
        }
    }
}
