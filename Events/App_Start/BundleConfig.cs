using System.Web;
using System.Web.Optimization;

namespace Events
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                      "~/node_modules/jquery/dist/jquery.js"));

            bundles.Add(new ScriptBundle("~/scripts/popper.js").Include(
                      "~/node_modules/popper.js/dist/umd/popper.js"));

            bundles.Add(new ScriptBundle("~/scripts/bootstrap").Include(
                      "~/node_modules/bootstrap/dist/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/scripts/js").Include(
                      "~/js/scripts.js",
                      "~/js/jquery.maskedinput.js"));

            bundles.Add(new StyleBundle("~/fontawesome/css").Include(
                      "~/node_modules/@fortawesome/fontawesome-free/css/all.css"));

            bundles.Add(new StyleBundle("~/content/css").Include(
                      "~/node_modules/bootstrap/compiler/bootstrap.css",
                      "~/content/css/style.css"));

            bundles.Add(new StyleBundle("~/custom/css").Include(
                      "~/content/css/forms.css"));
        }
    }
}
