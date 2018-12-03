using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Status.Core.Interfaces
{
    public interface IDatabaseConfig
    {
        string Sql { get; }
        string ConnectionString { get; }
    }
}
