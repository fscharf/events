using System.Web;
using System.Web.Optimization;

namespace Events
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/jquery").Include(
                      "~/node_modules/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/popper.js").Include(
                      "~/node_modules/popper.js/dist/umd/popper.min.js"));

            bundles.Add(new ScriptBundle("~/bootstrap/js").Include(
                      "~/node_modules/bootstrap/dist/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/iziToast/js").Include(
                      "~/node_modules/izitoast/dist/js/iziToast.min.js"));

            bundles.Add(new ScriptBundle("~/scripts/js").Include(
                      "~/scripts/scripts.js"));

            bundles.Add(new StyleBundle("~/styles/css").Include(
                      "~/node_modules/@fortawesome/fontawesome-free/css/all.min.css",
                      "~/node_modules/izitoast/dist/css/iziToast.min.css",
                      "~/content/css/style.min.css",
                      "~/content/PagedList.css"));
        }
    }
}
