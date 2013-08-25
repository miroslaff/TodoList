using System;
using System.Web.Optimization;

namespace TodoList.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(
                new ScriptBundle("~/App/scripts")
                    .Include("~/App/libs/jquery/jquery-{version}.js")
                    .Include("~/App/libs/bootstrap/bootstrap.js")

                    .Include("~/App/libs/angular/angular.js")
                    .Include("~/App/libs/angular/angular-resource.js")
                    .Include("~/App/libs/angular/angular-cookies.js")

                    .Include("~/App/libs/ui-bootstrap/ui-bootstrap-custom-0.5.0.js")
                    .Include("~/App/libs/ui-bootstrap/ui-bootstrap-custom-tpls-0.5.0.js")

                    .IncludeDirectory("~/App/libs/utils", "*.js")

                    .Include("~/App/app.js")

                    .IncludeDirectory("~/App/services", "*.js")
                    .IncludeDirectory("~/App/controllers", "*.js", true)
                    .IncludeDirectory("~/App/directives", "*.js")
                    .IncludeDirectory("~/App/filters", "*.js")
                    .IncludeDirectory("~/App/templates", "*.js")
            );

            bundles.Add(
                new ScriptBundle("~/Content/style")
                    .Include("~/Content/css/bootstrap.css")
                    .Include("~/Content/css/toastr.css")
                    .Include("~/Content/css/site.css")
            );
        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
            {
                throw new ArgumentNullException("ignoreList");
            }

            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
        }
    }
}