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
                new ContentFile("~/Script/Vendor/vendor.js"),
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