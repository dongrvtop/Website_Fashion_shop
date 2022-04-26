using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace BestFashionShop
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            // Default API Route put post get with ID
            config.Routes.MapHttpRoute("DefaultApi", "Api/{controller}/{id}", new { id = RouteParameter.Optional }, new { id = @"\d+" });
            // Default API Route put post get without ID
            config.Routes.MapHttpRoute("DefaultApiWithAction", "Api/{controller}/{action}");
            // Default API Route Post with controller multi-post Action
            config.Routes.MapHttpRoute("DefaultApiPost", "Api/{controller}/{action}", new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            config.Routes.MapHttpRoute("DefaultApiPostId", "Api/{controller}/{action}/{id}", new { Id = RouteParameter.Optional }, new { Id = @"\d+" });
            config.Routes.MapHttpRoute("DefaultApiPostIdString", "Api/{controller}/{action}/{id}");
        }
    }
}
