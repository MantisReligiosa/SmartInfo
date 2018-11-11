using System.Collections.Generic;
using Nancy.Bundle;

namespace Web.Bundles
{
    public class JsBundles : JSFiles
    {
        public override List<IContentItem> Contents()
        {
            return new List<IContentItem>()
            {
                new ContentFile("~/node_modules/jquery/dist/jquery.min.js"),
                //new ContentFolder("~/content/app",eRecursive.ThisFolderAndChildrenFolders, eMinify.MinifyIt)
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