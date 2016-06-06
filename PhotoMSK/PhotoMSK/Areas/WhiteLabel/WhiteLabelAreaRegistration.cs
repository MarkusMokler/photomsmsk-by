using System.Web.Mvc;

namespace PhotoMSK.Areas.WhiteLabel
{
    public class WhiteLabelAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "WhiteLabel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "WhiteLabel_default",
                "WhiteLabel/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}