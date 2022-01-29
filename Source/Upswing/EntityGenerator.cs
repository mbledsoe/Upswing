using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing
{
    public class EntityGenerator : IGenerator
    {
        private readonly IEntityFileModelBuilder entityFileModelBuilder;
        private readonly ITemplate<EntityFileModel> entityTemplate;
        private readonly IOutputWriter<EntityFileModel> outputWriter;

        public EntityGenerator(IEntityFileModelBuilder entityFileModelBuilder, ITemplate<EntityFileModel> entityTemplate, IOutputWriter<EntityFileModel> outputWriter)
        {
            this.entityFileModelBuilder = entityFileModelBuilder;
            this.entityTemplate = entityTemplate;
            this.outputWriter = outputWriter;
        }

        public void Generate(TableDefinition tableDef, string entityNamespace)
        {
            Console.WriteLine($"Generating for table {tableDef.TableName}");

            var entityFileModel = entityFileModelBuilder.BuildModel(tableDef, entityNamespace);
            var entityTemplateOutput = entityTemplate.Render(entityFileModel);
            outputWriter.WriteOutput(entityFileModel, entityTemplateOutput);
        }
    }

    public class DataAccessObjectGenerator : IGenerator
    {
        private readonly IDataAccessFileModelBuilder dataAccessObjectFileModelBuilder;
        private readonly ITemplate<DataAccessObjectFileModel> template;
        private readonly IOutputWriter<DataAccessObjectFileModel> outputWriter;

        public DataAccessObjectGenerator(IDataAccessFileModelBuilder dataAccessObjectFileModelBuilder, ITemplate<DataAccessObjectFileModel> template, IOutputWriter<DataAccessObjectFileModel> outputWriter)
        {
            this.dataAccessObjectFileModelBuilder = dataAccessObjectFileModelBuilder;
            this.template = template;
            this.outputWriter = outputWriter;
        }

        public void Generate(TableDefinition tableDef, string entityNamespace)
        {
            Console.WriteLine($"Generating for table {tableDef.TableName}");
            var fileModel = dataAccessObjectFileModelBuilder.BuildModel(tableDef, entityNamespace);
            var templateOutput = template.Render(fileModel);
            outputWriter.WriteOutput(fileModel, templateOutput);
        }
    }
}
