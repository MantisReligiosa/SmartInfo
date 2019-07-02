using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.TableProviders
{
    public class CsvTableProvider : ITableProvider
    {
        public string Extension => "csv";

        public IEnumerable<string> Header { get; private set; }

        public IEnumerable<IEnumerable<string>> Rows { get; private set; }

        public void LoadData(object context)
        {
            var linesSeparator = new char[] { '\r', '\n' };
            var itemSeparator = ',';

            var lines = ((string)context).Split(linesSeparator, StringSplitOptions.RemoveEmptyEntries);
            Header = new List<string>(lines.First().Split(itemSeparator));
            Rows = lines.Skip(1).Select(line => new List<string>(line.Split(itemSeparator)));
        }
    }
}
