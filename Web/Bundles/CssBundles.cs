using Nancy.Bundle;
using System.Collections.Generic;

namespace Web.Bundles
{
    public class CssBundles : CSSFiles
    {
        public override List<IContentItem> Contents()
        {
            return new List<Nancy.Bundle.IContentItem>()
            {
                new ContentFile("~/node_modules/bootstrap/dist/css/bootstrap.css"),
                new ContentFile("~/node_modules/toastr/build/toastr.min.css"),
                new ContentFile("~/Sources/jquery-easyui-1.6.7/themes/default/easyui.css"),
                new ContentFile("~/Sources/jquery-easyui-1.6.7/themes/icon.css"),
                new ContentFile("~/Sources/jquery-easyui-ribbon/ribbon.css"),
                new ContentFile("~/Sources/jquery-easyui-ribbon/ribbon-icon.css"),
                new ContentFile("~/css/styles.css", eMinify.MinifyIt),
                new ContentFile("~/css/images.css", eMinify.MinifyIt)
            };
        }

        public override string ReleaseKey()
        {
            return "css-key";
        }

        public override string ReleaseUrl()
        {
            return "/styles";
        }
    }
}