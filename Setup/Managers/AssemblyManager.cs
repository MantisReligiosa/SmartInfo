using Setup.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Setup.Managers
{
    public static class AssemblyManager
    {
        public static void GetAssemblyInfo(string assemblyPath,
            out Guid guid, out Version version)
        {
            var assembly = System.Reflection.Assembly.LoadFrom(assemblyPath);

            var guidValue = ((GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)
                .First()).Value;

            guid = new Guid(guidValue);
            version = new Version(assembly.GetName().Version.ToString(3));
        }

        public static IEnumerable<string> GetAssemblyPathsCollection(string assembliesLocation)
        {
            return Constants.DependentAssemblies
                .Select(lib => System.IO.Path.Combine(assembliesLocation, lib));
        }
    }
}
