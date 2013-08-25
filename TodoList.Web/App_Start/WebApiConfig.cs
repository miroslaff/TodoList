using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using TodoList.Web.Security;

namespace TodoList.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // 
            // Json.Net settings (we are overriding the defaults in order to support camel casing on the client side)

            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            //config.MessageHandlers.Add(new BasicAuthenticationHandler());

            //
            // Routes

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
