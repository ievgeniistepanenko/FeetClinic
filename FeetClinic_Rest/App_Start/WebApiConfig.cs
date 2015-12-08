using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace FeetClinic_Rest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "route1",
                routeTemplate: "api/{controller}/{therapistId}/{day}/{year}/{properties}",
                defaults: new { therapistId = RouteParameter.Optional,
                                day = RouteParameter.Optional,
                                year = RouteParameter.Optional,
                                properties = "" }
            );

            config.Routes.MapHttpRoute(
                name: "route2",
                routeTemplate: "api/{controller}/{id}/{properties}",
                defaults: new { id = RouteParameter.Optional, properties = "" }
            );

            config.Routes.MapHttpRoute(
                name: "route3",
                routeTemplate: "api/{controller}/{customerId}/{properties}",
                defaults: new { customerId = RouteParameter.Optional, properties = "" }
            );
            

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
