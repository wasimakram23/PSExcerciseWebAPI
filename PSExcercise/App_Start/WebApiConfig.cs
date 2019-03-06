using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using WebApiContrib.Formatting.Jsonp;
using PSExcercise.CustomFilter;

namespace PSExcercise
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //for jsonp cross domain resource sharing
            // var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            //config.Formatters.Insert(0, jsonpFormatter);

            //for cross origin resourse sharing
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            
            //For returning json data
             config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            // config.Formatters.Remove(config.Formatters.XmlFormatter);

            //Set custom HTTPS route filter
            //config.Filters.Add(new RouteToHTTPS());

            //Set basic authentication filter
            // config.Filters.Add(new BasicAuthenticationFilter());

            // For resolving camelCase 
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
