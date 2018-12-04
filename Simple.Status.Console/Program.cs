using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Simple.Status.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Core.Interfaces.IDatabaseConfig databaseConfig = new Core.DatabaseConfig();
            Core.Interfaces.IInputConfig inputConfig = new Core.InputConfig();
            Core.Interfaces.IOutputConfig outputConfig = new Core.OutputConfig();

            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("config/main.json");
            IConfiguration config = builder.Build();

            config.Bind("Database", databaseConfig);
            config.Bind("Input", inputConfig);
            config.Bind("Output", outputConfig);
            string connectionString = databaseConfig.ConnectionString;

            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                string sql = databaseConfig.Sql;
                string inputString = inputConfig.FormattedInput;
                List<string> outputStrings = new List<string>();
                List<string> columns = GetColumnsFromInputString(inputString);

                using (var command = new System.Data.SqlClient.SqlCommand(sql))
                {
                    command.Connection = connection;
                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Dictionary<string, string> entries = new Dictionary<string, string>();
                            string outputString = inputString;
                            foreach (var column in columns)
                            {
                                var val = reader.GetValue(reader.GetOrdinal(column));
                                entries.Add("{" + column + "}", val.ToString());

                                foreach (var entry in entries)
                                {
                                    outputString = outputString.Replace(entry.Key, entry.Value);
                                }
                            }
                            outputStrings.Add(outputString);
                        }
                    }

                    foreach (var outputString in outputStrings)
                    {
                        if (outputConfig.Multiline)
                            System.Console.WriteLine(outputString);
                        else
                            System.Console.Write($"{outputString}{outputConfig.Seperator}");

                    }
                }
            }

            System.Console.ReadKey();
        }


        private static List<string> GetColumnsFromInputString(string inputString)
        {
            Regex regex = new Regex(@"(?<=\{)[^}]*(?=\})", RegexOptions.IgnoreCase);
            List<string> matches = new List<string>();

            foreach (var match in regex.Matches(inputString))
            {
                matches.Add(match.ToString());
            }

            return matches;
        }
    }
}
