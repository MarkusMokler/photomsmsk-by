using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lucene.Net.Support;

namespace PhotoMSK.Classes
{

    public class ControllerArgs
    {
        private readonly string _controllerName;
        private readonly string _themeName;
        private readonly string _area;
        private HttpContextBase _httpContext;

        public string ControllerName { get { return _controllerName; } }
        public string ThemeName { get { return _themeName; } }
        public string Area { get { return _area ?? ""; } }
        public HttpContextBase HttpContext { get { return _httpContext; } }
        public string RouteName { get { return HttpContext.Items["routeName"] as string; } }

        /// <summary>
        /// Create the name for theme engine
        /// </summary>
        /// <param name="controllerContext">Controller context for theme</param>
        public ControllerArgs(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            _httpContext = controllerContext.HttpContext;
            _controllerName = controllerContext.RouteData.GetRequiredString("controller");

            if (controllerContext.IsChildAction == false && (_controllerName.Equals("Home", StringComparison.CurrentCultureIgnoreCase) || _controllerName.Equals("WhiteLabel", StringComparison.CurrentCultureIgnoreCase)))
            {
                _controllerName = controllerContext.HttpContext.Items["routeName"] as string ?? _controllerName;
            }
            _area = controllerContext.RouteData.DataTokens["area"] as string;

            var themeName = controllerContext.HttpContext.Items["themeName"] as string ?? "Default";

            _themeName = themeName;
        }

        public string GetChacheKey(ViewType prefix, string name)
        {
            return string.Format(CultureInfo.InvariantCulture, ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}:{5}", GetType().AssemblyQualifiedName, prefix, name, _controllerName, _themeName, _area);
        }
    }

    public enum ViewType
    {
        Partial, View, MasterPage
    }

    public class ViewLocation
    {
        public ViewType ViewType { get; set; }
        public string Path { get; set; }
        public bool Themed { get; set; }

        public string GetPath(ControllerArgs args, string viewName)
        {
            
            if (Themed)
                switch (ViewType)
                {
                    case ViewType.MasterPage:
                        return string.Format(Path, args.RouteName, args.ThemeName, args.ControllerName, viewName);
                    case ViewType.View:
                        return string.Format(Path, args.RouteName, args.ThemeName, args.ControllerName, viewName);
                    case ViewType.Partial:
                        return string.Format(Path, args.RouteName, args.ThemeName, args.ControllerName, viewName);
                    default:
                        throw new Exception();
                }
            switch (ViewType)
            {
                case ViewType.MasterPage:
                    return string.Format(Path, args.Area, args.ControllerName, viewName);
                case ViewType.View:
                    return string.Format(Path, args.Area, args.ControllerName, viewName);
                case ViewType.Partial:
                    return string.Format(Path, args.Area, args.ControllerName, viewName);
                default:
                    throw new Exception();
            }
        }
    }

    public class RazorThemeViewEngine : RazorViewEngine
    {

        public List<ViewLocation> ViewLocations = new List<ViewLocation>
        {
            new ViewLocation{Path = "~/Themes/{0}/{1}/Views/{2}/{3}.cshtml",Themed = true,ViewType = ViewType.MasterPage},
            new ViewLocation{Path = "~/Themes/{0}/{1}/Views/Shared/{3}.cshtml",Themed = true,ViewType = ViewType.MasterPage},
      
            new ViewLocation{Path = "~/Areas/{0}/Views/{1}/{2}.cshtml",Themed = false,ViewType = ViewType.MasterPage},
            new ViewLocation{Path = "~/Areas/{0}/Views/Shared/{2}.cshtml",Themed = false,ViewType = ViewType.MasterPage},

            new ViewLocation{Path = "~/Themes/{0}/{1}/Views/{2}/{3}.cshtml",Themed = true,ViewType = ViewType.View},
            new ViewLocation{Path = "~/Areas/{0}/Views/{1}/{2}.cshtml",Themed = false,ViewType = ViewType.View},


            new ViewLocation{Path = "~/Themes/{0}/{1}/Views/{2}/{3}.cshtml",Themed = true,ViewType = ViewType.Partial},
            new ViewLocation{Path = "~/Themes/{0}/{1}/Views/Templates/{3}.cshtml",Themed = true,ViewType = ViewType.Partial},

   
            new ViewLocation{Path =  "~/Areas/{0}/Views/{1}/{2}.cshtml",Themed = false,ViewType = ViewType.Partial},
            new ViewLocation{Path =  "~/Areas/{0}/Views/Shared/{2}.cshtml",Themed = false,ViewType = ViewType.Partial},
            new ViewLocation{Path =  "~/Areas/{0}/Views/Templates/{2}.cshtml",Themed = false,ViewType = ViewType.Partial},
            };

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return FileExists(new ControllerArgs(controllerContext), virtualPath);
        }
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            if (string.IsNullOrEmpty(viewName)) throw new ArgumentException("viewName must be specified.", "viewName");

            ControllerArgs controllerArgs = new ControllerArgs(controllerContext);

            var viewPath = GetPath(controllerArgs, ViewType.View, viewName, useCache);
            string masterPath = string.Empty;


            if (!string.IsNullOrEmpty(masterName))
            {
                masterPath = GetPath(controllerArgs, ViewType.MasterPage, masterName, useCache);
            }

            if (!string.IsNullOrEmpty(viewPath) && (!string.IsNullOrEmpty(masterPath) || string.IsNullOrEmpty(masterName)))
            {
                return new ViewEngineResult(CreateView(controllerContext, viewPath, masterPath), this);
            }

            throw new DirectoryNotFoundException(string.Format("Вьюха {1} не найдена для  {0} ", controllerArgs.ControllerName, viewName));
        }
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            if (string.IsNullOrEmpty(partialViewName)) throw new ArgumentException(@"partialViewName must be specified.", "partialViewName");

            var con = new ControllerArgs(controllerContext);

            var partialViewPath = GetPath(con, ViewType.Partial, partialViewName, useCache);

            return new ViewEngineResult(CreatePartialView(controllerContext, partialViewPath), this);
        }


        private bool FileExists(ControllerArgs controllerContext, string virtualPath)
        {
            try
            {
                return File.Exists(controllerContext.HttpContext.Server.MapPath(virtualPath));
            }
            catch (HttpException exception)
            {
                if (exception.GetHttpCode() != 0x194)
                {
                    throw;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private string GetPath(ControllerArgs controllerContext, ViewType viewType, string name, bool useCache)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Имя вида не задано");
            }


            if (!useCache)
                return IsSpecificPath(name)
                    ? GetPathFromSpecificName(controllerContext, viewType, name)
                    : GetPathFromGeneralName(controllerContext, viewType, name);


            string key = controllerContext.GetChacheKey(viewType, name);

            var viewLocation = ViewLocationCache.GetViewLocation(controllerContext.HttpContext, key);

            return viewLocation ?? (IsSpecificPath(name)
                ? GetPathFromSpecificName(controllerContext, viewType, name)
                : GetPathFromGeneralName(controllerContext, viewType, name));
        }

        private static bool IsSpecificPath(string name)
        {
            var ch = name[0];
            if (ch != '~')
            {
                return (ch == '/');
            }
            return true;
        }

        private string GetPathFromGeneralName(ControllerArgs controllerContext, ViewType viewType, string name)
        {
            var virtualPath = string.Empty;

            var locations = ViewLocations.Where(x => x.ViewType == viewType).ToList();
            if ((locations == null) || (locations.Count == 0))
            {
                throw new InvalidOperationException("locations must not be null or emtpy.");
            }

            List<string> sl = new List<string>(locations.Count);

            foreach (var loc in locations)
            {
                virtualPath = loc.GetPath(controllerContext, name);
                if (FileExists(controllerContext, virtualPath))
                {
                    ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, controllerContext.GetChacheKey(viewType, name), virtualPath);
                    return virtualPath;
                }

                sl.Add(virtualPath);

            }
            var str = string.Join("\n", sl);
            throw new DirectoryNotFoundException(str);
        }

        private string GetPathFromSpecificName(ControllerArgs controllerContext, ViewType viewType, string name)
        {
            var virtualPath = name;
            if (!FileExists(controllerContext, name))
            {
                throw new Exception(virtualPath);
            }
            ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, controllerContext.GetChacheKey(viewType, name), virtualPath);
            return virtualPath;
        }
    }
}