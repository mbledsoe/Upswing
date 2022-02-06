using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing
{
    public class CodeGenerator
    {
        private readonly ITableDefinitionSource tableDefinitionSource;
        private readonly ITableSpec tableSpec;

        public CodeGenerator(ITableDefinitionSource tableDefinitionSource, ITableSpec tableSpec)
        {
            this.tableDefinitionSource = tableDefinitionSource;
            this.tableSpec = tableSpec;
        }

        public void Run<T>(ITemplateModelBuilder<T> modelBuilder, ITemplate<T> template, IOutputWriter<ITemplateModel> outputWriter) where T : ITemplateModel
        {
            foreach (var tableDefinition in tableDefinitionSource.GetTableDefinitions())
            {
                if (tableSpec.IsMatch(tableDefinition))
                {
                    Console.WriteLine($"Generating for table [{tableDefinition.SchemaName}].[{tableDefinition.TableName}]");
                    var templateModel = modelBuilder.Build(tableDefinition);
                    var output = template.Render(templateModel);                    
                    outputWriter.Write(templateModel, output);
                }
            }
        }
    }
}
