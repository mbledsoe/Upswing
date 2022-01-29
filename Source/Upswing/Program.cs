using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using CommandLine;
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
            var entityGenerator = CreateEntityGenerator(options.Output);
            var dapperMapperGenerator = CreateDapperMapperGeneartor(options.Output);

            foreach (var tableDef in tableDefinitions)
            {                
                if (generatorTableSpec.IsMatch(tableDef))
                {
                    Console.WriteLine($"Generating entity file for table {tableDef.TableName}.");
                    entityGenerator.Generate(tableDef, options.Namespace);

                    Console.WriteLine($"Generating DapperMapper file for table {tableDef.TableName}");
                    dapperMapperGenerator.Generate(tableDef, options.Namespace);
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
                new ScribanTemplate<EntityFileModel>(new EmbeddedTemplateSource("Upswing.Scriban.Entity.scriban")),
                new FileOutputWriter(outputPath, new DefaultEntityFileNamingStrategy()));
        }

        static EntityGenerator CreateDapperMapperGeneartor(string outputPath)
        {
            return new EntityGenerator(
                new DefaultEntityFileModelBuilder(),
                new ScribanTemplate<EntityFileModel>(new EmbeddedTemplateSource("Upswing.Scriban.DapperMapper.scriban")),
                new FileOutputWriter(outputPath, new DefaultEntityFileNamingStrategy("{0}DapperMapper.generated.cs")));
        }
    }
}
