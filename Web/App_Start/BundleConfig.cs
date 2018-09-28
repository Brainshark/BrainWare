using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                     "~/Content/Scripts/jquery-{version}.js",
                     "~/Content/Scripts/orders.js",
                     "~/Content/Scripts/utility.js",
                     "~/Content/Scripts/bootstrap.js",
                     "~/Content/Scripts/respond.js",
                     "~/Content/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/Style/bootstrap.css",
                      "~/Content/Style/site.css"));
        }
    }
}
