using System.Web.Optimization;

namespace TopBloggers
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Lib/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Lib/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Lib/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/pagescripts").Include(
                "~/Scripts/animation.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/main-styles.css"));
        }
    }
}
