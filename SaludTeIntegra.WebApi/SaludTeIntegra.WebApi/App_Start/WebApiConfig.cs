using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;

namespace SaludTeIntegra.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API
            // Configure Web API para usar solo la autenticación de token de portador.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            #region  Login
            config.Routes.MapHttpRoute(
                name: "Login",
                routeTemplate: "api/Login",
                defaults: new
                {
                    controller = "Login"
                }
            );
            #endregion

            #region  Usuario
            config.Routes.MapHttpRoute(
                name: "Usuario",
                routeTemplate: "api/Usuario",
                defaults: new
                {
                    controller = "Usuario"
                }
            );
            #endregion

            #region  Visor
            config.Routes.MapHttpRoute(
                name: "Visor",
                routeTemplate: "api/Visor",
                defaults: new
                {
                    controller = "Visor"
                }
            );
            #endregion

            #region  Rol
            config.Routes.MapHttpRoute(
                name: "Rol",
                routeTemplate: "api/Rol",
                defaults: new
                {
                    controller = "Rol"
                }
            );
            #endregion

            #region  EntidadContratante
            config.Routes.MapHttpRoute(
                name: "EntidadContratante",
                routeTemplate: "api/EntidadContratante",
                defaults: new
                {
                    controller = "EntidadContratante"
                }
            );
            #endregion

            #region  Ryf
            config.Routes.MapHttpRoute(
                name: "Ryf",
                routeTemplate: "api/Ryf",
                defaults: new
                {
                    controller = "Ryf"
                }
            );
            #endregion

            #region  Clave
            config.Routes.MapHttpRoute(
                name: "Clave",
                routeTemplate: "api/Clave",
                defaults: new
                {
                    controller = "Clave"
                }
            );
            #endregion

            #region  Impresion
            config.Routes.MapHttpRoute(
                name: "Impresion",
                routeTemplate: "api/Impresion",
                defaults: new
                {
                    controller = "Impresion"
                }
            );
            #endregion

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
