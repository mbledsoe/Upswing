using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using CommandLine;
using Scriban;
using Upswing;

namespace Upswing
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<UpswingOptions>(args).WithParsed(RunProgram);
        }

        static void RunProgram(UpswingOptions options)
        {
            using (var conn = new SqlConnection(options.ConnectionString))
            {
                conn.Open();

                var dao = new SqlServerMetadataDao(conn);
                var tableDefinitions = dao.GetTableDefinitions();
                var entityRenderer = new EntityFileRenderer();

                ITableSpec tableSpec = (options.Tables.Any()) ? new MatchOnNameTableSpec(options.Tables.ToList()) : new MatchAllTableSpec();

                foreach (var tableDef in tableDefinitions)
                {
                    if (tableSpec.IsMatch(tableDef))
                    {
                        Console.WriteLine($"Generating for table {tableDef.TableName}.");
                        entityRenderer.Render(tableDef, options.Output, options.Namespace);
                    }                    
                }
            }
        }
    }
}
