using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Setup.CustomDialogs
{
    public partial class ConnectionStringInput : UserControl
    {
        private ConnectionStringInputDataContext _dataContext = new ConnectionStringInputDataContext();

        /// <summary>
        /// Содержит строку подключения согласно заполненным полям
        /// </summary>
        [Description("Содержит строку подключения согласно заполненным полям")]
        public string ConnectionString
        {
            get
            {
                return _dataContext.ConnectionString;
            }
            set
            {
                _dataContext.ConnectionString = value;
            }
        }

        public ConnectionStringInput()
        {
            InitializeComponent();
            connectionStringInputDataContextBindingSource.DataSource = _dataContext;
        }
    }
}
