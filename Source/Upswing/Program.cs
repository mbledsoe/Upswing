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
            var generationProcess = new GenerationProcess();

            Console.WriteLine($"Generating Entity files.");
            var entityGenerator = CreateEntityGenerator(options.Output);
            generationProcess.Run(tableDefinitionSource, tableSpec, entityGenerator, options.Namespace);
            Console.WriteLine($"Finished generating Entity files.");

            Console.WriteLine($"Generating DapperMapper files.");
            var dapperMapperGenerator = CreateDapperMapperGenerator(options.Output);
            generationProcess.Run(tableDefinitionSource, tableSpec, dapperMapperGenerator, options.Namespace);
            Console.WriteLine($"Finished generating DapperMapper files.");

            /*
             * Commenting these out for now... I'll come back and refactor some of this tomorrow.
             * 
            Console.WriteLine($"Generating Interface DAO files.");
            var interfaceDaoGenerator = CreateInterfaceDaoGenerator(options.Output);
            generationProcess.Run(tableDefinitionSource, tableSpec, interfaceDaoGenerator, options.Namespace);
            Console.WriteLine($"Finished generating Interface DAO files.");

            Console.WriteLine($"Generating SqlServer DAO files.");
            var sqlServerDaoGenerator = CreateSqlServerDaoGenerator(options.Output);
            generationProcess.Run(tableDefinitionSource, tableSpec, sqlServerDaoGenerator, options.Namespace);
            Console.WriteLine($"Finished generating SqlServer DAO files.");
            */
        }

        private static ITableSpec GetGeneratorTableSpec(IEnumerable<string> tables)
        {
            return tables.Any() ? new MatchOnNameTableSpec(tables.ToList()) : new MatchAllTableSpec();
        }

        private static IGenerator CreateInterfaceDaoGenerator(string outputPath)
        {
            return new DataAccessObjectGenerator(
                new DefaultDataAccessFileModelBuilder(new DefaultNameTransformer(), new DefaultDatabaseConventions()),
                new ScribanTemplate<DataAccessObjectFileModel>(new EmbeddedTemplateSource("Upswing.Scriban.InterfaceDao.scriban")),
                new DataAccessFileOutputWriter(outputPath, "I{0}Dao.generated.cs"));
        }

        private static IGenerator CreateSqlServerDaoGenerator(string outputPath)
        {
            return new DataAccessObjectGenerator(
                new DefaultDataAccessFileModelBuilder(new DefaultNameTransformer(), new DefaultDatabaseConventions()),
                new ScribanTemplate<DataAccessObjectFileModel>(new EmbeddedTemplateSource("Upswing.Scriban.SqlServerDao.scriban")),
                new DataAccessFileOutputWriter(outputPath));                
        }

        static IGenerator CreateEntityGenerator(string outputPath)
        {
            return new EntityGenerator(
                new DefaultEntityFileModelBuilder(new SingletonNameTransformer()),
                new ScribanTemplate<EntityFileModel>(new EmbeddedTemplateSource("Upswing.Scriban.Entity.scriban")),
                new EntityFileOutputWriter(outputPath, new DefaultEntityFileNamingStrategy()));
        }

        static IGenerator CreateDapperMapperGenerator(string outputPath)
        {
            return new EntityGenerator(
                new DefaultEntityFileModelBuilder(new SingletonNameTransformer()),
                new ScribanTemplate<EntityFileModel>(new EmbeddedTemplateSource("Upswing.Scriban.DapperMapper.scriban")),
                new EntityFileOutputWriter(outputPath, new DefaultEntityFileNamingStrategy("{0}DapperMapper.generated.cs")));
        }
    }
}
