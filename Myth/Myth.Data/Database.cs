using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myth.Data
{
    public class Database
    {
        public static DbConnection GetOpenConnection(string connectionStringName)
        {
            var connString = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connString == null)
            {
                return null;
            }

            var factory = DbProviderFactories.GetFactory(connString.ProviderName);
            var conn = factory.CreateConnection();
            conn.ConnectionString = connString.ConnectionString;
            conn.Open();
            return conn;
        }
    }
}
