using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Status.Core
{
    public class DatabaseConfig : Interfaces.IDatabaseConfig
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConnectionStringTemplate { get; set; }
        public string Sql { get; set; }

        public string BuildConnectionString()
        {
            string connectionString = ConnectionStringTemplate.Replace("{username}", Username);
            connectionString = connectionString.Replace("{password}", Password);

            return connectionString;
        }
    }
}
