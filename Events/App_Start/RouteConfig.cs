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
                "Eventos",
                "Eventos",
                new { controller = "Events", action = "Index" }
            );

            routes.MapRoute(
                "MeusEventos",
                "MeusEventos",
                new { controller = "Events", action = "MyEvents" }
            );

            routes.MapRoute(
                "Conta",
                "Conta/{id}",
                new { controller = "Users", action = "MyProfile" }
            );
            
            routes.MapRoute(
                "Entrar",
                "Entrar",
                new { controller = "Users", action = "Login" }
            );
            
            routes.MapRoute(
                "Cadastro",
                "Cadastro",
                new { controller = "Users", action = "Register" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
