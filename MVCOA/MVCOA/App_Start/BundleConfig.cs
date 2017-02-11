using System.Web;
using System.Web.Optimization;

namespace MVCOA
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"
                        ));

            //合并JQuery脚本的请求，让他们一起发送到浏览器，提高效率。
            bundles.Add(new ScriptBundle("~/mvcAjax").Include(
                "~/Scripts/jquery-1.8.2.min.js", //JQuery插件
                "~/Scripts/jquery.unobtrusive-ajax.min.js",//微软JQuery的ajax插件
                "~/Scripts/jquery.validate.js",//JQuery验证插件
                "~/Scripts/jquery.validate.unobtrusive.js",//微软JQuery的非侵入式验证插件
                "~/Scripts/jquery.msgProcess.js"//自己写的一个
                ));

            //关于EasyUI的一些JS文件
            bundles.Add(new ScriptBundle("~/easyUIJS").Include(
                "~/Scripts/jquery.min.js",
                "~/EasyUI/jquery.easyui.min.js",
                "~/Scripts/jquery.msgProcess.js",
                "~/EasyUI/locale/easyui-lang-zh_CN.js"
                ));

            //关于EasyUI的一些CSS文件
            //    //因为css中可能使用到了 相对路径的图片，所以 需要为 虚拟路径 指定到 能找到图片的 路径
            bundles.Add(new StyleBundle("~/EasyUI/themes/default/easyuiCSS").Include(
                    "~/EasyUI/themes/default/easyui.css"

                ));
            bundles.Add(new StyleBundle("~/EasyUI/themes/iconCSS").Include(
                 "~/EasyUI/themes/icon.css"
                ));
                           

            //开启合并
            BundleTable.EnableOptimizations = true;

        }
    }
}