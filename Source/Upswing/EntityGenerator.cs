using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing
{
    public class EntityGenerator
    {
        private readonly IEntityFileModelBuilder entityFileModelBuilder;
        private readonly IEntityTemplate entityTemplate;
        private readonly IOutputWriter outputWriter;

        public EntityGenerator(IEntityFileModelBuilder entityFileModelBuilder, IEntityTemplate entityTemplate, IOutputWriter outputWriter)
        {
            this.entityFileModelBuilder = entityFileModelBuilder;
            this.entityTemplate = entityTemplate;
            this.outputWriter = outputWriter;
        }

        public void Generate(TableDefinition tableDef, string entityNamespace)
        {
            var entityFileModel = entityFileModelBuilder.BuildModel(tableDef, entityNamespace);
            var entityTemplateOutput = entityTemplate.Render(entityFileModel);
            outputWriter.WriteOutput(entityFileModel, entityTemplateOutput);
        }
    }
}
