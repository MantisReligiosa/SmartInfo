using System.Collections.Generic;
using Nancy.Bundle;

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
                new ContentFile("~/css/styles.css", eMinify.MinifyIt)
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