using Nancy.Bundle.Settings;

namespace Web.Bundles
{
    public class MyBundleConfig : DefaultConfigSettings
    {
        public override string CommonAssetsRoute
        {
            get
            {
                return "/cli-bundles";
            }
        }
    }
}