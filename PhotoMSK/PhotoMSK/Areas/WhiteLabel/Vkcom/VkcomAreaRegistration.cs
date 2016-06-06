using System.Web.Mvc;

namespace PhotoMSK.Areas.vkcom
{
    public class VkcomAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "vkcom";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Vkcom_default",
                "Vkcom/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}