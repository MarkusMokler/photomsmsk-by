﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoMSK.Areas.Default
{
    public class WhiteLabelRoute : Route
    {
        /// <summary>
        /// Gets or sets the URL pattern for the domain route.
        /// </summary>
        public string Domain { get; private set; }

        /// <summary>
        /// The regular expression used to match on the domain tokens to remove in <see cref="RemoveDomainTokens"/>.
        /// </summary>
        private readonly Regex _tokenRegex =
                new Regex(@"({[a-zA-Zа-яА-Я0-9_]*})*-?\.?\/?({[a-zA-Zа-яА-Я0-9_]*})*-?\.?\/?({[a-zA-Zа-яА-Я0-9_]*})*-?\.?\/?({[a-zA-Zа-яА-Я0-9_]*})*-?\.?\/?({[a-zA-Zа-яА-Я0-9_]*})*-?\.?\/?({[a-zA-Zа-яА-Я0-9_]*})*-?\.?\/?({[a-zA-Zа-яА-Я0-9_]*})*-?\.?\/?({[a-zA-Zа-яА-Я0-9_]*})*-?\.?\/?({[a-zA-Zа-яА-Я0-9_]*})*-?\.?\/?({[a-zA-Zа-яА-Я0-9_]*})*-?\.?\/?({[a-zA-Zа-яА-Я0-9_]*})*-?\.?\/?({[a-zA-Zа-яА-Я0-9_]*})*-?\.?\/?");

        /// <summary>
        /// The regular expression used to match the domain route parameters.
        /// </summary>
        private readonly Regex _domainRegex;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRoute"/> class,
        /// by using the specified <paramref name="domain"/> and <paramref name="url"/>
        /// patterns and default parameter values.
        /// </summary>
        /// <param name="domain">The domain pattern for the route.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        public WhiteLabelRoute(string domain, string url, object defaults)
            : this(domain, url, new RouteValueDictionary(defaults), new MvcRouteHandler())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRoute"/> class,
        /// by using the specified <paramref name="domain"/> and <paramref name="url"/>
        /// patterns, default parameter values and handler class
        /// </summary>
        /// <param name="domain">The domain pattern for the route.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public WhiteLabelRoute(string domain, string url, object defaults, IRouteHandler routeHandler)
            : this(domain, url, new RouteValueDictionary(defaults), routeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRoute"/> class,
        /// by using the specified <paramref name="domain"/> and <paramref name="url"/>
        /// patterns and default parameter values.
        /// </summary>
        /// <param name="domain">The domain pattern for the route.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use for any parameters that are missing in the URL.</param>
        public WhiteLabelRoute(string domain, string url, RouteValueDictionary defaults)
            : this(domain, url, defaults, new MvcRouteHandler())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRoute"/> class,
        /// by using the specified <paramref name="domain"/> and <paramref name="url"/>
        /// patterns, default parameter values and handler class
        /// </summary>
        /// <param name="domain">The domain pattern for the route.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use for any parameters that are missing in the URL.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public WhiteLabelRoute(string domain, string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
            Domain = domain;
            _domainRegex = CreatePatternRegex(Domain);
        }

        /// <summary>
        /// Returns information about the requested route.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
        /// <returns>An object that contains the values from the route definition.</returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            // Request information
            string requestDomain = httpContext.Request.Headers["host"];

            if (requestDomain.Contains("photomsk"))
                return null;
            if (requestDomain.Contains("xn--n1aalg.xn--90ais"))
                return null;
            if (requestDomain.Contains("fotomsk"))
                return null;
            if (requestDomain.Contains("ngweb"))
                return null;
            if (requestDomain.Contains("localhost"))
                return null;

            if (!string.IsNullOrWhiteSpace(requestDomain))
            {
                if (requestDomain.IndexOf(":", StringComparison.Ordinal) > 0)
                {
                    requestDomain = requestDomain.Substring(0, requestDomain.IndexOf(":", StringComparison.Ordinal));
                }
            }
            else
            {
                if (httpContext.Request.Url == null) return null;
                requestDomain = httpContext.Request.Url.Host;
            }

            // Match domain and route
            Match domainMatch = _domainRegex.Match(requestDomain);
            if (domainMatch.Success == false) return null;

            RouteData data = base.GetRouteData(httpContext);
            if (data == null) return null;

            // Iterate matching domain groups
            for (int i = 1; i < domainMatch.Groups.Count; i++)
            {
                Group group = domainMatch.Groups[i];
                if (group.Success)
                {
                    string key = _domainRegex.GroupNameFromNumber(i);

                    if (!string.IsNullOrEmpty(key) && !char.IsNumber(key, 0))
                    {
                        if (!string.IsNullOrEmpty(group.Value))
                        {
                            data.Values[key] = group.Value;
                        }
                    }
                }
            }
            data.DataTokens["Area"] = "Default";
            data.DataTokens.Add("namespaces", new string[] { "PhotoMSK.Areas.Default.Controllers" });
            return data;
        }

        /// <summary>
        /// Returns information about the URL that is associated with the route.
        /// </summary>
        /// <param name="requestContext">An object that encapsulates information about the requested route.</param>
        /// <param name="values">An object that contains the parameters for a route.</param>
        /// <returns>An object that contains information about the URL that is associated with the route.</returns>
        /// <remarks>Any domain tokens are removes to prevent them affecting the virtual path.</remarks>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return base.GetVirtualPath(requestContext, RemoveDomainTokens(values));
        }

        /// <summary>
        /// Creates the regular expression of the route <paramref name="pattern"/>.
        /// </summary>
        /// <param name="pattern">The route pattern.</param>
        /// <returns>a regular expression of the route <paramref name="pattern"/>.</returns>
        private static Regex CreatePatternRegex(string pattern)
        {
            // Perform replacements
            var sb = new StringBuilder(pattern);
            sb.Replace("/", @"\/?")
                .Replace("-", @"\-?")
                .Replace("{", @"(?<")
                .Replace("}", @">([a-zA-Zа-яА-Я0-9_.-]*))");

            return new Regex($"^{sb}$");
        }

        /// <summary>
        /// Removes any domain tokens from the values.
        /// </summary>
        /// <param name="values">An object that contains the parameters for a route.</param>
        /// <returns>The values with any domain tokens removed.</returns>
        private RouteValueDictionary RemoveDomainTokens(RouteValueDictionary values)
        {
            Match tokenMatch = _tokenRegex.Match(Domain);
            foreach (Group group in tokenMatch.Groups)
            {
                if (group.Success)
                {
                    string key = group.Value.Replace("{", "").Replace("}", "");
                    if (values.ContainsKey(key))
                        values.Remove(key);
                }
            }

            return values;
        }
    }
}