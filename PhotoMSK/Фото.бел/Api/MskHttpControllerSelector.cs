using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Fotobel.Api
{
    public class MskHttpControllerSelector : IHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;
        private readonly ConcurrentDictionary<string, HttpControllerDescriptor> _controllerv1;
        private readonly ConcurrentDictionary<string, HttpControllerDescriptor> _controllerv2;
        private static readonly string _prefix = "Controller";

        public virtual HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var routeData = request.GetRouteData();

            var controllerName = routeData.Values["controller"].ToString();
            var version = string.IsNullOrEmpty((string)routeData.Values["version"]) ? "Services" : (string)routeData.Values["version"];
            var pc = (string)routeData.Values["parentController"];

            switch (version)
            {
                case "Services":
                    return _controllerv1["Services." + controllerName];
                case "2":
                    string fullname;
                    if (string.IsNullOrEmpty(pc))
                        fullname = controllerName;
                    else
                        fullname = pc + "." + controllerName;
                    return _controllerv2["v2." + fullname];
            }

            return null;
        }

        public virtual IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return _controllerv1.Concat(_controllerv2).ToDictionary(x => x.Key, x => x.Value);
        }

        private void InitializeControllerInfoCache()
        {
            var list = GetType().Assembly.DefinedTypes.Where(x => typeof(ApiController).IsAssignableFrom(x)).ToList();
            foreach (var type in list)
            {
                Debug.Assert(type.Namespace != null, "type.Namespace != null");

                if (type.Namespace.Contains("Services"))
                {
                    string key = "Services." + type.Name.Replace(_prefix, "");
                    _controllerv1.TryAdd(key, new HttpControllerDescriptor(_configuration, key, type));

                }
                if (type.Namespace.Contains("Version2"))
                {
                    string key = type.FullName.Replace("Fotobel.Api.Version2", "v2").Replace(_prefix, "");
                    _controllerv2.TryAdd(key, new HttpControllerDescriptor(_configuration, key, type));
                }

            }
        }


        public MskHttpControllerSelector(HttpConfiguration configuration)
        {
            _configuration = configuration;
            _controllerv1 = new ConcurrentDictionary<string, HttpControllerDescriptor>(StringComparer.InvariantCultureIgnoreCase);
            _controllerv2 = new ConcurrentDictionary<string, HttpControllerDescriptor>(StringComparer.InvariantCultureIgnoreCase);
            InitializeControllerInfoCache();
        }
    }
}
