using Nancy.Bundle;
using System.Collections.Generic;

namespace Web.Bundles
{
    public class MyCustomCssBundle : CSSFiles
    {
        public override List<IContentItem> Contents()
        {
            return new List<Nancy.Bundle.IContentItem>()
            {
                new ContentFile("~/css/styles.css", eMinify.MinifyIt)
            };
        }

        public override string ReleaseKey()
        {
            return "my-css";
        }

        public override string ReleaseUrl()
        {
            return "/x-css-url";
        }
    }
}