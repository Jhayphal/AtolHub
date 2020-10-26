using System;
using System.Collections.Generic;
using System.Text;

namespace AtolHub.Core.Data
{
    public class MongoDBDataProvider : IDataProvider
    {
        private string _connectionString { get; set; }

        public MongoDBDataProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region Methods

        /// <summary>
        /// Initialize database
        /// </summary>
        public virtual void InitDatabase()
        {
            DataSettingsHelper.InitConnectionString(_connectionString);
        }

        #endregion
    }
}
