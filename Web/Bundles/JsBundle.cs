using Nancy.Bundle;
using System.Collections.Generic;
using System.Linq;

namespace Web.Bundles
{
    public class JsBundle : JSFiles
    {
        private readonly string[] _files;
        private readonly string _releaseKey;
        private readonly string _releaseUrl;
        public JsBundle(string releaseKey, string releaseUrl, params string[] files)
        {
            _releaseKey = releaseKey;
            _releaseUrl = releaseUrl;
            _files = files;
        }

        public override List<IContentItem> Contents()
        {
            return _files.Select(f => new ContentFile(f, eMinify.DoNotMinifyIt)).ToList<IContentItem>();
        }

        public override string ReleaseKey()
        {
            return _releaseKey;
        }

        public override string ReleaseUrl()
        {
            return _releaseUrl;
        }
    }
}