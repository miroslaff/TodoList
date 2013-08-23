using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace TodoList.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // 
            // Json.Net settings (we are overriding the defaults in order to support camel casing on the client side)

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

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
