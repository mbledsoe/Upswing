using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandLine;
using Upswing;
using Upswing.CSharp;
using Upswing.Dapper;
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
            GenerateDapperDaos(options, tableDefinitionSource, tableSpec);
            GenerateDapperStartup(options, tableDefinitionSource, tableSpec);
        }

        private static void GenerateDapperDaos(UpswingOptions options, SqlServerTableDefinitionSource tableDefinitionSource, ITableSpec tableSpec)
        {
            var modelBuilder = new DapperDaoModelBuilder(options.Namespace, new SingletonNameTransformer());

            var template = new ScribanTemplate<DapperDaoModel>(
                new EmbeddedTemplateSource(Assembly.GetAssembly(typeof(CSharpEntity)),
                "Upswing.Dapper.Templates.DapperDao.scriban"));

            var outputWriter = new FileOutputWriter(options.Output, new SimpleFileNameFormat("{0}.generated.cs"));
            
            foreach (var table in tableDefinitionSource.GetTableDefinitions())
            {
                if (tableSpec.IsMatch(table))
                {
                    var model = modelBuilder.Build(table);
                    var output = template.Render(model);
                    outputWriter.Write(model, output);
                }
            }
        }

        private static void GenerateDapperStartup(UpswingOptions options, SqlServerTableDefinitionSource tableDefinitionSource, ITableSpec tableSpec)
        {
            var entityModelBuilder = new CSharpEntityBuilder(options.Namespace, new SingletonNameTransformer());

            var template = new ScribanTemplate<CSharpEntityList>(
                new EmbeddedTemplateSource(Assembly.GetAssembly(typeof(CSharpEntity)),
                "Upswing.Dapper.Templates.DapperStartup.scriban"));

            var outputWriter = new FileOutputWriter(options.Output, new SimpleFileNameFormat("DapperStartup.generated.cs"));
            var entityList = new CSharpEntityList();
            entityList.Namespace = options.Namespace;

            foreach (var table in tableDefinitionSource.GetTableDefinitions())
            {
                if (tableSpec.IsMatch(table))
                {
                    var entity = entityModelBuilder.Build(table);
                    entityList.Entities.Add(entity);
                }
            }

            var output = template.Render(entityList);
            outputWriter.Write(entityList, output);
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
