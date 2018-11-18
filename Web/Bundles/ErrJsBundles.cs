using System.Collections.Generic;
using Nancy.Bundle;

namespace Web.Bundles
{
    public class ErrJsBundles : JSFiles
    {
        public override List<IContentItem> Contents()
        {
            return new List<IContentItem>()
            {
                new ContentFile("~/script/app/errForm.viewmodel.js", eMinify.DoNotMinifyIt)
            };
        }

        public override string ReleaseKey()
        {
            return "err-js-key";
        }

        public override string ReleaseUrl()
        {
            return "/err";
        }
    }
}