using Nancy.Bundle;
using System.Collections.Generic;

namespace Web.Bundles
{
    public class MasterJsBundle : JSFiles
    {
        public override List<IContentItem> Contents()
        {
            return new List<IContentItem>()
            {
                new ContentFile("~/script/app/master.viewmodel.js", eMinify.DoNotMinifyIt),
                new ContentFile("~/script/app/textBlockEdit.viewmodel.js", eMinify.DoNotMinifyIt),
                new ContentFile("~/script/app/tableBlockEdit.viewmodel.js", eMinify.DoNotMinifyIt),
                new ContentFile("~/script/app/pictureBlockEdit.viewmodel.js", eMinify.DoNotMinifyIt),
                new ContentFile("~/script/app/datetimeBlockEdit.viewmodel.js", eMinify.DoNotMinifyIt),
                new ContentFile("~/script/app/position.viewmodel.js", eMinify.DoNotMinifyIt),
            };
        }

        public override string ReleaseKey()
        {
            return "master-js-key";
        }

        public override string ReleaseUrl()
        {
            return "/master";
        }
    }
}