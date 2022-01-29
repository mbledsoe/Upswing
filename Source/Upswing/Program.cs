using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using CommandLine;
using Scriban;
using Upswing;
using Upswing.Scriban;

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
            var tableDefinitions = GetTableDefinitions(options.ConnectionString);
            var generatorTableSpec = GetGeneratorTableSpec(options.Tables);
            var generator = CreateEntityGenerator(options.Output);

            foreach (var tableDef in tableDefinitions)
            {
                Console.WriteLine($"Generating entity file for table {tableDef.TableName}.");

                if (generatorTableSpec.IsMatch(tableDef))
                {
                    generator.Generate(tableDef, options.Namespace);
                }
            }
        }

        private static ITableSpec GetGeneratorTableSpec(IEnumerable<string> tables)
        {
            return tables.Any() ? new MatchOnNameTableSpec(tables.ToList()) : new MatchAllTableSpec();
        }

        private static IList<TableDefinition> GetTableDefinitions(string connString)
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var dao = new SqlServerMetadataDao(conn);
                return dao.GetTableDefinitions();
            }
        }

        static EntityGenerator CreateEntityGenerator(string outputPath)
        {
            return new EntityGenerator(
                new DefaultEntityFileModelBuilder(),
                new ScribanEntityTemplate(new EmbeddedTemplateSource("Upswing.Scriban.Entity.scriban")),
                new FileOutputWriter(outputPath, new DefaultEntityFileNamingStrategy()));
        }
    }
}
