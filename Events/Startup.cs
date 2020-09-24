using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Events.Startup))]

namespace Events
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Users/Login"),
                ExpireTimeSpan = TimeSpan.FromMinutes(30),
                CookieName = "ApplicationCookie",
                LogoutPath = new PathString("/Users/Logout")
            });
        }
    }
}
