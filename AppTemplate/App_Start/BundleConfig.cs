using System.Web;
using System.Web.Optimization;

namespace AppTemplate
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/ionicons.css",
                      "~/Content/font-awesome/css/font-awesome.css",
                      "~/Content/AdminLTE/css/AdminLTE.css",
                      "~/Content/AdminLTE/css/skins/_all-skins.css",
                        //"~/Content/AdminLTE/plugins/datatables/dataTables.bootstrap.css",
                        //"~/Content/AdminLTE/plugins/datatables/jquery.dataTables.css",
                        //"~/Content/AdminLTE/plugins/datatables/jquery.dataTables.min.css",
                        //"~/Content/AdminLTE/plugins/datatables/jquery.dataTables_themeroller.css",
                      "~/Content/AdminLTE/plugins/jvectormap/jquery-jvectormap-1.2.2.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/AdminLTE").Include(
                      "~/Content/AdminLTE/js/app.js",

                        //"~/Content/AdminLTE/plugins/datatables/dataTables.bootstrap.js",
                        //"~/Content/AdminLTE/plugins/datatables/dataTables.bootstrap.min.js",
                        //"~/Content/AdminLTE/plugins/datatables/jquery.dataTables.min.js",
                        //"~/Content/AdminLTE/plugins/datatables/jquery.dataTables.js",                    
                      "~/Content/AdminLTE/plugins/jQuery/jQuery-2.2.0.min.js",
                      "~/Content/AdminLTE/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                      "~/Content/AdminLTE/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                      "~/Content/AdminLTE/plugins/daterangepicker/daterangepicker.js",
                      "~/Scripts/bootbox.min.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/ejscripts").Include(
                           "~/Scripts/jsrender.min.js",
                           "~/Scripts/jquery.easing-1.3.min.js",
                            "~/Scripts/ej/ej.web.all.min.js",
                            "~/Scripts/ej/ej.unobtrusive.min.js"));
            bundles.Add(new StyleBundle("~/bundles/ejstyles").Include(
                      "~/ejThemes/flat-saffron/ej.widgets.all.min.css"));

        }
    }
}
