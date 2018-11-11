﻿using System.Collections.Generic;
using Nancy.Bundle;

namespace Web.Bundles
{
    public class CssBundles : CSSFiles
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