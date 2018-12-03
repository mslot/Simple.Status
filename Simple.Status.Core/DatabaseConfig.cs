using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Status.Core
{
    public class DatabaseConfig : Interfaces.IDatabaseConfig
    {
        public string ConnectionString { get; set; }
        public string Sql { get; set; }
    }
}
