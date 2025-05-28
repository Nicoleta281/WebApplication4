using System.Web;
using System.Web.Optimization;

namespace WebApplication4
{
     public class BundleConfig
     {
          public static void RegisterBundles(BundleCollection bundles)
          {
               bundles.Add(new StyleBundle("~/Content/css").Include(
                   "~/Content/css/animate.css",
                   "~/Content/css/owl.carousel.min.css",
                   "~/Content/css/owl.theme.default.min.css",
                   "~/Content/css/magnific-popup.css",
                   "~/Content/css/bootstrap-datepicker.css",
                   "~/Content/css/jquery.timepicker.css",
                   "~/Content/css/flaticon.css", // <- foarte important
                   "~/Content/css/style.css",
                   "~/Content/css/Site.css" // opțional dacă ai stiluri locale
               ));
               // JS
               bundles.Add(new ScriptBundle("~/bundles/js").Include(
                   "~/Scripts/js/jquery-3.2.1.min.js",
                   "~/Scripts/js/jquery-migrate-3.0.1.min.js",
                   "~/Scripts/js/popper.min.js",
                   "~/Scripts/js/bootstrap.min.js",
                   "~/Scripts/js/jquery.easing.1.3.js",
                   "~/Scripts/js/jquery.waypoints.min.js",
                   "~/Scripts/js/jquery.stellar.min.js",
                   "~/Scripts/js/owl.carousel.min.js",
                   "~/Scripts/js/jquery.magnific-popup.min.js",
                   "~/Scripts/js/jquery.animateNumber.min.js",
                   "~/Scripts/js/bootstrap-datepicker.js",
                   "~/Scripts/js/jquery.timepicker.min.js",
                   "~/Scripts/js/scrollax.min.js",
                   "~/Scripts/js/google-map.js",
                   "~/Scripts/js/main.js"
               ));
               BundleTable.EnableOptimizations = false;
          }
     }
}

