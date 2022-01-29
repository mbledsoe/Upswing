﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing
{
    public class DefaultEntityFileModelBuilder : IEntityFileModelBuilder
    {
        private readonly IPropertyNameTransformer propertyNameTransformer;

        public DefaultEntityFileModelBuilder()
            :this(new DefaultPropertyNameTransformer())
        {
        }

        public DefaultEntityFileModelBuilder(IPropertyNameTransformer propertyNameTransformer)
        {
            this.propertyNameTransformer = propertyNameTransformer;
        }

        public EntityFileModel BuildModel(TableDefinition tableDef, string entityNamespace)
        {
            return new EntityFileModel
            {
                EntityName = tableDef.TableName,
                Namespace = entityNamespace,
                Properties = BuildEntityFileProperties(tableDef),
                Table = tableDef
            };
        }

        private IList<EntityFileProperty> BuildEntityFileProperties(TableDefinition tableDef)
        {
            return tableDef.Columns.Select(c => new EntityFileProperty
            {
                Name = propertyNameTransformer.TransformName(c),
                ClrType = SqlUtils.GetClrTypeName(c),
                Column = c
            }).ToList();
        }
    }
}
