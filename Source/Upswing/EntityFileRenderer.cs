using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Scriban;

namespace Upswing
{
    public class EntityFileRenderer
    {
        public void Render(TableDefinition tableDef, string outputPath, string entityNamespace)
        {
            using (var rs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Upswing.Entity.scriban"))
            {
                using (var sr = new StreamReader(rs))
                {
                    var entityTemplateSource = new StreamReader(rs).ReadToEnd();
                    var entityTemplate = Template.Parse(entityTemplateSource);

                    var entityFileModel = BuildEntityFileModel(tableDef, entityNamespace);
                    var output = entityTemplate.Render(entityFileModel);

                    var entityFilePath = GetFilename(outputPath, tableDef);
                    File.WriteAllText(entityFilePath, output);
                }
            }
        }

        private string GetFilename(string outputPath, TableDefinition tableDef)
        {
            return Path.Combine(outputPath, $"{tableDef.TableName}.generated.cs");
        }

        private static EntityFileModel BuildEntityFileModel(TableDefinition tableDef, string entityNamespace)
        {
            return new EntityFileModel
            {
                EntityName = tableDef.TableName,
                Namespace = entityNamespace,
                Properties = BuildEntityFileProperties(tableDef)
            };
        }

        private static IList<EntityFileProperty> BuildEntityFileProperties(TableDefinition tableDef)
        {
            return tableDef.Columns.Select(c => new EntityFileProperty
            {
                Name = c.ColumnName,
                TypeName = SqlGenerationUtils.GetClrTypeName(c)
            }).ToList();
        }
    }
}
