using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Simple.Status.Console
{
    /*
     * DISCLAIMER
     * This code is a going to be refactored, a lot. I have a simple data collector
     * that collect sensor data from around my house. I want to see if I can display the collected
     * data in my i3 display manager (Linux).
     */
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
                var outputStrings = new List<string>();
                var columns = GetColumnsFromInputString(inputString);

                using (var command = new System.Data.SqlClient.SqlCommand(sql))
                {
                    command.Connection = connection;
                    connection.Open();

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var entries = new Dictionary<string, string>();
                            string outputString = inputString;
                            foreach (var column in columns)
                            {
                                var val = reader.GetValue(reader.GetOrdinal(column));

                                if(val != null)
                                {
                                    string key = "{" + column + "}";
                                    entries.Add(key, val.ToString());
                                }

                                foreach (var entry in entries)
                                {
                                    string outputKey = entry.Key;
                                    string outputValue = entry.Value;
                                    outputString = outputString.Replace(outputKey, outputValue);
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
            var matches = new List<string>();

            foreach (var match in regex.Matches(inputString))
            {
                matches.Add(match.ToString());
            }

            return matches;
        }
    }
}
