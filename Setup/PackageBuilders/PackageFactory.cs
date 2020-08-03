using Setup.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using WixSharp.Bootstrapper;

namespace Setup.Packages
{
    internal class PackageFactory : IPackageFactory
    {
        private readonly List<IPackageBuilder> _builders;
        public PackageFactory(List<IPackageBuilder> builders)
        {
            _builders = builders;
        }

        public ChainItem[] GetPackages(Guid guid, Version version)
        {
            return _builders.Select(b => b.Make(guid, version)).ToArray();
        }

        public void Cleanup()
        {
            _builders.ForEach(b => b.Cleanup());
        }
    }
}
