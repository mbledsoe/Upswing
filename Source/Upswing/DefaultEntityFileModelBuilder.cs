using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing
{
    public class DefaultEntityFileModelBuilder : IEntityFileModelBuilder
    {
        private readonly INameTransformer nameTransformer;

        public DefaultEntityFileModelBuilder()
            :this(new DefaultNameTransformer())
        {
        }

        public DefaultEntityFileModelBuilder(INameTransformer nameTransformer)
        {
            this.nameTransformer = nameTransformer;            
        }

        public EntityFileModel BuildModel(TableDefinition tableDef, string entityNamespace)
        {
            return new EntityFileModel
            {
                EntityName = nameTransformer.TransformTableName(tableDef),
                Namespace = entityNamespace,
                Properties = BuildEntityFileProperties(tableDef),
                Table = tableDef
            };
        }

        private IList<EntityFileProperty> BuildEntityFileProperties(TableDefinition tableDef)
        {
            return tableDef.Columns.Select(c => new EntityFileProperty
            {
                Name = nameTransformer.TransformColumnName(c),
                ClrType = SqlUtils.GetClrTypeName(c),
                Column = c
            }).ToList();
        }
    }
}
