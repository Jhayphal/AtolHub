using System;
using System.Collections.Generic;
using System.Text;

namespace AtolHub.Core.Data
{
    public static class DataSettingsHelper
    {
        private static string _connectionString;
        /// <summary>
        /// Returns a value indicating whether database is already installed
        /// </summary>
        /// <returns></returns>

        public static void InitConnectionString(string connectionString)
        {
            var manager = new DataSettingsManager();
            var settings = manager.LoadSettings(connectionString);

            if (!String.IsNullOrEmpty(settings.DataConnectionString))
                _connectionString = settings.DataConnectionString;
        }

        public static string ConnectionString()
        {
            return _connectionString;
        }
    }
}
