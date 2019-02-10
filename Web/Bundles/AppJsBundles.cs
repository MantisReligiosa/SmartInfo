using System.Collections.Generic;
using Nancy.Bundle;

namespace Web.Bundles
{
    public class AppJsBundles : JSFiles
    {
        public override List<IContentItem> Contents()
        {
            return new List<IContentItem>()
            {
                new ContentFile("~/script/app/_run.js", eMinify.DoNotMinifyIt),
                new ContentFile("~/script/app/app.viewmodel.js", eMinify.DoNotMinifyIt)
            };
        }

        public override string ReleaseKey()
        {
            return "app-js-key";
        }

        public override string ReleaseUrl()
        {
            return "/app";
        }
    }
}