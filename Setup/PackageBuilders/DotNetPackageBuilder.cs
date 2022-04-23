using Setup.Interfaces;
using System;
using WixSharp.Bootstrapper;

namespace Setup.Packages
{
    internal class DotNetPackageBuilder : IPackageBuilder
    {
        public ChainItem Make(Guid guid, Version version)
        {
            return new PackageGroupRef("NetFx462Web");
        }
    }
}
