using System.Web.Optimization;

namespace OnlineStore.UI.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //-------------ScriptBundle
            //Below we add a bundle that will load our JS script tags in the Layout
            bundles.Add(new ScriptBundle("~/bundles/template").Include(
                "~/Scripts/jquery.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/DataTables/jquery.dataTables.min.js",//added after installing Datatables using Nuget
                "~/Scripts/main.js"
                ));

            //-------------StyleBundle--------------
            //Below we modified the ~/Content/css bundle and added the files from our template into the Include method which, when this styleBundle is called in the Layout, will load each of these files in a link tag in the head of the document. The BundleConfig makes loading these resources much more efficient and should be utilized accordingly.
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/DataTables/css/jquery.dataTables.min.css",//added after installing Datatables using Nuget
                      "~/Content/PagedList.css", //added after installing PagedList.Mvc via Nuget
                      "~/Content/css/style.css"
                      ));
        }
    }
}
