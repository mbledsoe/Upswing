using System.Collections.Generic;
using System.Linq;

namespace Upswing.CSharp
{
    public class CSharpEntityBuilder : ITemplateModelBuilder<CSharpEntity>
    {
        private readonly string namespaceName;
        private readonly INameTransformer nameTransformer;        

        public CSharpEntityBuilder(string namespaceName, INameTransformer nameTransformer)
        {
            this.namespaceName = namespaceName;
            this.nameTransformer = nameTransformer;
        }

        public CSharpEntity Build(TableDefinition tableDefinition)
        {
            return new CSharpEntity
            {
                Namespace = namespaceName,
                Name = nameTransformer.TransformTableName(tableDefinition),
                Properties = BuildProperties(tableDefinition),
                SourceTable = tableDefinition
            };
        }

        private IList<CSharpEntityProperty> BuildProperties(TableDefinition tableDefinition)
        {
            return tableDefinition.Columns.Select(c => new CSharpEntityProperty
            {
                ClrType = ClrTypeMapper.GetClrType(c),
                IsNullable = c.IsNullable,
                Name = nameTransformer.TransformColumnName(c),
                SourceColumn = c
            }).ToList();
        }
    }
}
