using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandLine;
using Upswing;
using Upswing.CSharp;
using Upswing.Scriban;

namespace Upswing.ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<UpswingOptions>(args).WithParsed(RunProgram);
        }

        static void RunProgram(UpswingOptions options)
        {
            var tableDefinitionSource = new SqlServerTableDefinitionSource(options.ConnectionString);
            var tableSpec = GetGeneratorTableSpec(options.Tables);

            GenerateCSharpEntities(options, tableDefinitionSource, tableSpec);
            GenerateDapperMappers(options, tableDefinitionSource, tableSpec);
        }

        private static void GenerateDapperMappers(UpswingOptions options, SqlServerTableDefinitionSource tableDefinitionSource, ITableSpec tableSpec)
        {
            var generator = new CodeGenerator(tableDefinitionSource, tableSpec);

            var entityModelBuilder = new CSharpEntityBuilder(options.Namespace, new SingletonNameTransformer());

            var template = new ScribanTemplate<CSharpEntity>(
                new EmbeddedTemplateSource(Assembly.GetAssembly(typeof(CSharpEntity)),
                "Upswing.Dapper.Templates.DapperMapper.scriban"));
            
            var outputWriter = new FileOutputWriter(options.Output, new SimpleFileNameFormat("{0}DapperMapper.generated.cs"));

            Console.WriteLine("Generating Dapper Mappers");
            generator.Run(entityModelBuilder, template, outputWriter);
            Console.WriteLine("Done Generating Dapper Mappers");
            Console.WriteLine();
        }

        private static void GenerateCSharpEntities(UpswingOptions options, SqlServerTableDefinitionSource tableDefinitionSource, ITableSpec tableSpec)
        {
            var generator = new CodeGenerator(tableDefinitionSource, tableSpec);

            var entityModelBuilder = new CSharpEntityBuilder(options.Namespace, new SingletonNameTransformer());

            var template = new ScribanTemplate<CSharpEntity>(
                new EmbeddedTemplateSource(Assembly.GetAssembly(typeof(CSharpEntity)),
                "Upswing.CSharp.Templates.CSharpEntity.scriban"));
                        
            var outputWriter = new FileOutputWriter(options.Output, new SimpleFileNameFormat());

            Console.WriteLine("Generating Entities");
            generator.Run(entityModelBuilder, template, outputWriter);
            Console.WriteLine("Done Generating Entities");
            Console.WriteLine();
        }

        private static ITableSpec GetGeneratorTableSpec(IEnumerable<string> tables)
        {
            return tables.Any() ? new MatchOnNameTableSpec(tables.ToList()) : new MatchAllTableSpec();
        }
    }
}
