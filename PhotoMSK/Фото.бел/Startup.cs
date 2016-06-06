using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Fotobel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;

[assembly: OwinStartup(typeof(Startup), "Configuration")]
namespace Fotobel
{
    public class Startup
    {
        static Startup()
        {
            OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/system/account/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };
        }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            ConfigureOAuth(app);
            app.UseWebApi(config);
            app.MapSignalR();
        }

        public void ConfigureOAuth(IAppBuilder app)
        {


            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        public static OAuthAuthorizationServerOptions OAuthServerOptions;

    }

    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        UserManager<User> UserManager { get; set; }

        public SimpleAuthorizationServerProvider()
        {
            UserManager = new UserManager<User>(new UserStore<User>(new AppContext()));
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });


            IdentityUser user = await UserManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                using (AppContext _context = new AppContext())
                {
                    string number = context.UserName.Replace("-", "").Replace(" ", "");

                    if (number.StartsWith("80"))
                    {
                        var regex = new Regex(Regex.Escape("80"));
                        number = regex.Replace(number, "+375", 1);
                    }
                    if (number.StartsWith("+") == false)
                    {
                        number = "+" + number;
                    }

                    int a;

                    if (int.TryParse(context.Password, out a))
                    {
                        var phones = _context.Phones.Where(x => x.Number == number && x.UserPhone != null && x.UserPhone.ConfirmCode == a);
                        user = phones.Select(x => x.UserPhone.User.User).FirstOrDefault();

                    }
                    else
                    {
                        var phones = _context.Phones.Where(x => x.Number == number && x.UserPhone != null && x.UserPhone.Confirm);
                        
                        var username = phones.Select(x => x.UserPhone.User.User.UserName).FirstOrDefault();
                        if (username != null)
                        {
                            user = await UserManager.FindAsync(username, context.Password);
                        }
                        else
                        {
                            context.SetError("invalid_grant", "The user name or password is incorrect.");
                            return;
                        }

                    }
                }
            }


            UserInformation userInformation;
            using (var appContext = new AppContext())
            {
                userInformation = appContext.UserInformations.Single(x => x.User.Id == user.Id);

            }



            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            identity.AddClaim(new Claim("UserInformationID", userInformation.ID.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));

            context.Validated(identity);

        }
    }

}
