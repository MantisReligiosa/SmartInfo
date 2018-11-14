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
                new ContentFolder("~/Script/app",eRecursive.ThisFolderAndChildrenFolders, eMinify.DoNotMinifyIt)
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