using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhotoMSK.Startup))]
namespace PhotoMSK
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
