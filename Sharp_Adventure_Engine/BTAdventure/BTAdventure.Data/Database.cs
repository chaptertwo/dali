using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Data
{
    public static class Database
    {
        public static DbConnection GetOpenConnection(string connectionStringName)
        {
            var connString = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connString == null)
            {
                throw new Exception($"Could not find connection {connectionStringName}.");
            }

            var factory = DbProviderFactories.GetFactory(connString.ProviderName);
            var conn = factory.CreateConnection();
            conn.ConnectionString = connString.ConnectionString;
            conn.Open();
            return conn;
        }
    }
}
