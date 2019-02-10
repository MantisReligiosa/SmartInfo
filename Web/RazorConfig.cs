using Nancy.ViewEngines.Razor;
using System.Collections.Generic;

namespace Web
{
    public class RazorConfig : IRazorConfiguration
    {
        public bool AutoIncludeModelNamespace => true;

        public IEnumerable<string> GetAssemblyNames()
        {
            yield return "Web";
        }

        public IEnumerable<string> GetDefaultNamespaces()
        {
            yield return "Web";
        }
    }
}