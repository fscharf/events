using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Events
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "entrar",
                "entrar",
                new { controller = "Main", action = "Login" }
            );

            routes.MapRoute(
                "autenticar",
                "autenticar",
                new { controller = "Main", action = "Authorize" }
            );

            routes.MapRoute(
                "sair",
                "sair",
                new { controller = "Main", action = "Logout" }
            );

            routes.MapRoute(
                "cadastro",
                "cadastro",
                new { controller = "Cadastros", action = "Cadastro" }
            );

            routes.MapRoute(
                "cadastro_criar",
                "cadastro/criar",
                new { controller = "Cadastros", action = "Criar" }
            );

            routes.MapRoute(
                "eventos",
                "eventos",
                new { controller = "Eventos", action = "Index" }
            );
            
            routes.MapRoute(
                "configurar",
                "configurar",
                new { controller = "Settings", action = "Settings" }
            );

            routes.MapRoute(
                "eventos_nomedoevento",
                "eventos/nomedoevento",
                new { controller = "Eventos", action = "Evento" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
