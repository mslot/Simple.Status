using Microsoft.Extensions.Configuration;
using System;

namespace Simple.Status.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Core.Interfaces.IDatabaseConfig databaseConfig = new Core.DatabaseConfig();

            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables();
            IConfiguration config = builder.Build();

            config.Bind("Database", databaseConfig);

            string connectionString = databaseConfig.BuildConnectionString();

            System.Console.WriteLine(connectionString);
            System.Console.ReadKey();
        }
    }
}
