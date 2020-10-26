using System;
using System.Collections.Generic;
using System.Text;

namespace AtolHub.Core.Data
{
    public interface IDataProvider
    {
        /// <summary>
        /// Initialize database
        /// </summary>
        void InitDatabase();

    }
}
