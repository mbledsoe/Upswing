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
            var tableDefinitionSource = new SqlServerTableDefinitionSource(options.ConnectionString);
            var tableSpec = GetGeneratorTableSpec(options.Tables);
            var entityGenerator = CreateEntityGenerator(options.Output);
            var dapperMapperGenerator = CreateDapperMapperGeneartor(options.Output);

            var generationProcess = new GenerationProcess();

            Console.WriteLine($"Started generating Entity files.");
            generationProcess.Run(tableDefinitionSource, tableSpec, entityGenerator, options.Namespace);
            Console.WriteLine($"Finished generating Entity files.");

            Console.WriteLine($"Started generating DapperMapper files.");
            generationProcess.Run(tableDefinitionSource, tableSpec, dapperMapperGenerator, options.Namespace);
            Console.WriteLine($"Finished generating DapperMapper files.");
        }

        private static ITableSpec GetGeneratorTableSpec(IEnumerable<string> tables)
        {
            return tables.Any() ? new MatchOnNameTableSpec(tables.ToList()) : new MatchAllTableSpec();
        }

        private static ITableDefinitionSource CreateTableDefinitionSource(string connString)
        {
            return new SqlServerTableDefinitionSource(connString);
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
