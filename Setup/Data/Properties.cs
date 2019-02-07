using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup.Data
{
    public class Properties
    {
        internal static CustomProperty ConnectionString =>
            new CustomProperty(nameof(ConnectionString),
                @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Display-control;Integrated Security=True");
    }
}
