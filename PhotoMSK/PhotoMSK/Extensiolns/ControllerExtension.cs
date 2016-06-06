using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoMSK.Data.Models;


namespace PhotoMSK.Extensiolns
{
    /// <summary>
    /// Extends controller to setup themes and routes
    /// </summary>
    public static class ControllerExtension
    {
        /// <summary>
        /// Setup routeName to acces a file location
        /// </summary>
        /// <param name="controller">this contoller, to called to set route</param>
        /// <param name="etity">entity to type defined</param>
        public static void SetRoute(this Controller controller, RouteEntity etity)
        {
            var requestContext = controller.Request.RequestContext;

            var baseType = etity.GetType().BaseType;
            if (baseType != null)
                requestContext.HttpContext.Items["routeName"] = baseType.Name;
        }

        /// <summary>
        /// Setup theme to whitelabel
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="etity"></param>
        /// <param name="ignorewl">Ignore check WhiteLabel property in route </param>
        public static void SetTheme(this Controller controller, RouteEntity etity, bool ignorewl = false)
        {
            var requestContext = controller.Request.RequestContext;
            var baseType = etity.GetType().BaseType;
            if (baseType == null) return;

            requestContext.HttpContext.Items["routeName"] = baseType.Name;

            if (etity.WhiteLabel || ignorewl)
            {
                requestContext.HttpContext.Items["themeName"] = etity.Theme;
            }
        }
    }
}