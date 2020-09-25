using System.Data.Entity.Infrastructure;
using System.Web;
using System.Web.Mvc;

namespace Events
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute
            {
                Roles = "" + 1 + "," + 2 + "," + 3 + "," + 4 + "," + 5
            });
            filters.Add(new OutputCacheAttribute
            {
                Duration = 0,
                VaryByParam = "*",
                NoStore = true
            });
        }
    }
}
