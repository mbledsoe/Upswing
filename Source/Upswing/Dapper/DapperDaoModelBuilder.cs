using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing.Dapper
{
    public class DapperDaoModelBuilder
    {
        private readonly string namespaceName;
        private readonly INameTransformer nameTransformer;        

        public DapperDaoModelBuilder(string namespaceName, INameTransformer nameTransformer)
        {
            this.namespaceName = namespaceName;
            this.nameTransformer = nameTransformer;
        }

        public DapperDaoModel Build(TableDefinition tableDefinition)
        {
            return new DapperDaoModel
            {
                Namespace = namespaceName,
                Name = $"{nameTransformer.TransformTableName(tableDefinition)}Dao",
                EntityName = nameTransformer.TransformTableName(tableDefinition),
                IdentityColumnName = tableDefinition.IdentityColumn.ColumnName,
                IdentityPropertyName = nameTransformer.TransformColumnName(tableDefinition.IdentityColumn),
                SelectAllSql = BuildSelectAllSql(tableDefinition),
                SelectByIdSql = BuildSelectByIdSql(tableDefinition),
                InsertSql = BuildInsertSql(tableDefinition),
                UpdateSql = BuildUpdateSql(tableDefinition),
                DeleteSql = BuildDeleteSql(tableDefinition)
            };
        }

        private string BuildSelectByIdSql(TableDefinition tableDefinition)
        {
            var selectColumnList = string.Join(",", tableDefinition.Columns.Select(c => $"[{c.ColumnName}]"));

            return $"select {selectColumnList} from [{tableDefinition.SchemaName}].[{tableDefinition.TableName}] where [{tableDefinition.IdentityColumn.ColumnName}] = @{nameTransformer.TransformColumnName(tableDefinition.IdentityColumn)}";
        }

        private string BuildSelectAllSql(TableDefinition tableDefinition)
        {
            var selectColumnList = string.Join(",", tableDefinition.Columns.Select(c => $"[{c.ColumnName}]"));

            return $"select {selectColumnList} from [{tableDefinition.SchemaName}].[{tableDefinition.TableName}] order by [{tableDefinition.IdentityColumn.ColumnName}] offset @Offset rows fetch next @PageSize rows only";
        }

        private string BuildDeleteSql(TableDefinition tableDefinition)
        {
            return $"delete from [{tableDefinition.SchemaName}].[{tableDefinition.TableName}] where [{tableDefinition.IdentityColumn.ColumnName}] = @{nameTransformer.TransformColumnName(tableDefinition.IdentityColumn)}";
        }

        private string BuildUpdateSql(TableDefinition tableDefinition)
        {
            var setList = string.Join(",", tableDefinition.MutableColumns.Select(c => $"[{c.ColumnName}] = @{nameTransformer.TransformColumnName(c)}"));

            return $"update [{tableDefinition.SchemaName}].[{tableDefinition.TableName}] set {setList} where [{tableDefinition.IdentityColumn.ColumnName}] = @{nameTransformer.TransformColumnName(tableDefinition.IdentityColumn)}";
        }

        private string BuildInsertSql(TableDefinition tableDefinition)
        {
            var insertColumnList = string.Join(",", tableDefinition.MutableColumns.Select(c => $"[{c.ColumnName}]"));
            var insertParameterList = string.Join(",", tableDefinition.MutableColumns.Select(c => $"@{nameTransformer.TransformColumnName(c)}"));
            
            return $@"insert into [{tableDefinition.SchemaName}].[{tableDefinition.TableName}] ({insertColumnList}) values ({insertParameterList}); select scope_identity();";
        }
    }
}
