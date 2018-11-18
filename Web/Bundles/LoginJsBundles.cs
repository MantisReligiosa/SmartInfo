using Nancy.Bundle;
using System.Collections.Generic;

namespace Web.Bundles
{
    public class LoginJsBundles : JSFiles
    {
        public override List<IContentItem> Contents()
        {
            return new List<IContentItem>()
            {
                new ContentFile("~/script/app/login.viewmodel.js", eMinify.DoNotMinifyIt)
            };
        }

        public override string ReleaseKey()
        {
            return "login-js-key";
        }

        public override string ReleaseUrl()
        {
            return "/login";
        }
    }
}