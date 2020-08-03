using System;
using WixSharp.Bootstrapper;

namespace Setup.Interfaces
{
    internal interface IPackageFactory
    {
        ChainItem[] GetPackages(Guid guid, Version version);
        void Cleanup();
    }
}
