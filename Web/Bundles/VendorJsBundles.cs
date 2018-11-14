using Nancy.Bundle;
using System.Collections.Generic;

namespace Web.Bundles
{
    public class VendorJsBundles : JSFiles
    {
        public override List<IContentItem> Contents()
        {
            return new List<IContentItem>()
            {
                new ContentFile("~/node_modules/jquery/dist/jquery.min.js"),
                new ContentFile("~/node_modules/knockout/build/output/knockout-latest.js"),
                new ContentFile("~/node_modules/bootstrap/dist/js/bootstrap.min.js"),
                new ContentFile("~/node_modules/toastr/build/toastr.min.js"),
                new ContentFile("~/node_modules/finchjs/finch.min.js"),
                new ContentFile("~/Sources/jquery-easyui-1.6.7/jquery.easyui.min.js"),
                new ContentFile("~/Sources/jquery-easyui-ribbon/jquery.ribbon.js"),
            };
        }

        public override string ReleaseKey()
        {
            return "vendor-js-key";
        }

        public override string ReleaseUrl()
        {
            return "/vendor";
        }
    }
}