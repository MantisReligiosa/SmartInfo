namespace Web.Bundles
{
    using Nancy.Bundle.Settings;

    namespace Web.Bundles
    {
        public class BundleConfig : DefaultConfigSettings
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