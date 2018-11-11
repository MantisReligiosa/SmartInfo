using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Bundles
{
    using Nancy.Bundle.Settings;

    namespace Web.Bundles
    {
        public class MyBundleConfig : DefaultConfigSettings
        {
            public override string CommonAssetsRoute
            {
                get
                {
                    return "/bundles";
                }
            }
        }
    }
}