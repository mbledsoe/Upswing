using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Upswing
{
    public class DefaultDataAccessFileModelBuilder : IDataAccessFileModelBuilder
    {
        private readonly INameTransformer propertyNameTransformer;
        private readonly IDatabaseConventions databaseConventions;

        public DefaultDataAccessFileModelBuilder(INameTransformer propertyNameTransformer,
            IDatabaseConventions databaseConventions)
        {
            this.propertyNameTransformer = propertyNameTransformer;
            this.databaseConventions = databaseConventions;
        }

        public DataAccessObjectFileModel BuildModel(TableDefinition tableDef, string outputNamespace)
        {
            return new DataAccessObjectFileModel
            {
                EntityName = tableDef.TableName,
                Namespace = outputNamespace,
                IdColumn = databaseConventions.GetIdColumn(tableDef),
                InsertSql = GetInsertSql(tableDef),
                InsertParameters = GetInsertParameters(tableDef),
                UpdateSql = GetUpdateSql(tableDef),
                UpdateParameters = GetUpdateParameters(tableDef),
                DeleteSql = GetDeleteSql(tableDef),
                SelectSql = GetSelectSql(tableDef),
                Properties = GetProperties(tableDef)
            };
        }

        private IList<EntityFileProperty> GetProperties(TableDefinition tableDef)
        {
            return tableDef.Columns.Select(c => new EntityFileProperty
            {
                Column = c,
                IsNullable = c.IsNullable,
                Name = propertyNameTransformer.TransformColumnName(c),
                ClrType = SqlUtils.GetClrTypeName(c)                
            }).ToList();            
        }

        private string GetSelectSql(TableDefinition tableDef)
        {
            var idColumn = databaseConventions.GetIdColumn(tableDef);
            return $"select * from [{tableDef.TableName}] where [{idColumn.ColumnName}] = @{idColumn.ColumnName}";
        }

        private string GetDeleteSql(TableDefinition tableDef)
        {
            var idColumn = databaseConventions.GetIdColumn(tableDef);
            return $"delete from [{tableDef.TableName}] where [{idColumn.ColumnName}] = @{idColumn.ColumnName}";
        }

        private string GetUpdateSql(TableDefinition tableDef)
        {
            var idColumn = databaseConventions.GetIdColumn(tableDef);
            return $"update [{tableDef.TableName}] set {GetUpdateColumns(tableDef)} where {idColumn.ColumnName} = @{idColumn.ColumnName}";
        }

        private string GetUpdateColumns(TableDefinition tableDef)
        {
            return string.Join(",", GetMutableColumns(tableDef).Select(c => $"[{c.ColumnName}] = @{c.ColumnName}"));
        }

        private string GetInsertSql(TableDefinition tableDef)
        {
            var insertParameterList = string.Join(",", GetInsertParameters(tableDef).Select(c => $"@{c.Name}"));
            return $"insert into [{tableDef.TableName}] ({GetInsertColumns(tableDef)}) values ({insertParameterList}); select scope_identity();";
        }

        private IEnumerable<ColumnDefinition> GetMutableColumns(TableDefinition tableDef)
        {
            var idPropertyName = databaseConventions.GetIdColumn(tableDef).ColumnName;

            return tableDef.Columns
                .Where(c => string.Compare(c.ColumnName, idPropertyName, true) != 0);
        }

        private string GetInsertColumns(TableDefinition tableDef)
        {
            return string.Join(",", GetMutableColumns(tableDef).Select(c => $"[{c.ColumnName}]"));
        }

        private IList<DataAccessObjectFileModelSqlParameter> GetInsertParameters(TableDefinition tableDef)
        {
            return GetMutableColumns(tableDef).Select(c => new DataAccessObjectFileModelSqlParameter
            {
                Name = c.ColumnName,
                Size = c.MaxLength,
                SqlDbType = GetSqlDbType(c),
                ValueAccessor = propertyNameTransformer.TransformColumnName(c)
            }).ToList();
        }

        private IList<DataAccessObjectFileModelSqlParameter> GetUpdateParameters(TableDefinition tableDef)
        {
            var updateParameters = GetMutableColumns(tableDef).Select(c => new DataAccessObjectFileModelSqlParameter
            {
                Name = c.ColumnName,
                Size = c.MaxLength,
                SqlDbType = GetSqlDbType(c),
                ValueAccessor = propertyNameTransformer.TransformColumnName(c)
            }).ToList();

            var idColumn = databaseConventions.GetIdColumn(tableDef);

            updateParameters.Add(new DataAccessObjectFileModelSqlParameter
            {
                Name = idColumn.ColumnName,
                Size = idColumn.MaxLength,
                SqlDbType = GetSqlDbType(idColumn),
                ValueAccessor = propertyNameTransformer.TransformColumnName(idColumn)
            });

            return updateParameters;
        }

        private string GetSqlDbType(ColumnDefinition column)
        {
            switch (column.SystemTypeId)
            {
                case 56:
                    return "SqlDbType.Int";
                case 61:
                    return "SqlDbType.DateTime";
                case 104:
                    return "SqlDbType.Bit";
                case 106:
                    return "SqlDbType.Decimal";
                case 127:
                    return "SqlDbType.BigInt";
                case 35:
                    return "SqlDbType.Text";
                case 99:
                    return "SqlDbType.NText";
                case 167:
                    return "SqlDbType.VarChar";
                case 175:
                    return "SqlDbType.Char";
                case 231:
                    return "SqlDbType.NVarChar";
                default:
                    throw new Exception($"Unknown Column Type: {column.SystemTypeId}");
            }
        }
    }
}
