using System;
using WixSharp.Bootstrapper;

namespace Setup.Interfaces
{
    internal interface IPackageBuilder
    {
        ChainItem Make(Guid guid, Version version);
        void Cleanup();
    }
}
