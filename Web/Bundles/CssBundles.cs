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
                new ContentFile("~/css/vendor.css", eMinify.MinifyIt),
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