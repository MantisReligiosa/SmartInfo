using Setup.Interfaces;
using System;
using WixSharp.Bootstrapper;

namespace Setup.PackageBuilders
{
    internal class MigrationPackageBuilder : IPackageBuilder
    {
        public void Cleanup()
        {
            throw new NotImplementedException();
        }

        public ChainItem Make(Guid guid, Version version)
        {
            throw new NotImplementedException();
        }
    }
}
