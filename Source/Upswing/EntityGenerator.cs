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
        private readonly IOutputWriter outputWriter;

        public EntityGenerator(IEntityFileModelBuilder entityFileModelBuilder, ITemplate<EntityFileModel> entityTemplate, IOutputWriter outputWriter)
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
}
