using System.Collections.Generic;

namespace ServiceInterfaces
{
    public interface ITableProvider
    {
        string Extension { get; }

        IEnumerable<string> Header { get; }

        IEnumerable<IEnumerable<string>> Rows { get; }

        void LoadData(object context);
    }
}
