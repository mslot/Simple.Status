using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Status.Core.Interfaces
{
    public interface IDatabaseConfig
    {
        string Sql { get; }
        string Username { get; }
        string Password { get; }
        string ConnectionStringTemplate { get; }

        string BuildConnectionString(); //TODO: move logic out in second phase
    }
}
