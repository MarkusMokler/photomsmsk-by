using Autofac;
using PhotoMSK.ViewModels.Autofac;
using IContainer = Autofac.IContainer;


namespace Calendar_importer
{
    public static class AutofacConfig
    {
        /// <summary>
        /// Starts the application
        /// </summary>
        public static IContainer Register()
        {
            var builder = new ContainerBuilder();
            RegisterServices(builder);
            var container = builder.Build();


            // Set the dependency resolver for MVC.
            //var mvcResolver = new AutofacDependencyResolver(container);
            //DependencyResolver.SetResolver(mvcResolver);


            return container;

        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            AutofacRegistrator.RegisterTypes(builder);
        }
    }

}