using System.Collections.Generic;
using Nancy.Bundle;

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
                new ContentFile("~/node_modules/toastr/build/toastr.min.js")
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