using System;
using System.Collections.Generic;
using System.Text;

namespace AtolHub.Core.Data
{
    public partial class DataSettings
    {
        /// <summary>
        /// Data provider
        /// </summary>
        public string DataProvider { get; set; }

        /// <summary>
        /// Connection string
        /// </summary>
        public string DataConnectionString { get; set; }
    }
}
