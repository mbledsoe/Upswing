using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Upswing
{
    class SqlServerMetadataDao
    {
        private readonly SqlConnection conn;

        public SqlServerMetadataDao(SqlConnection conn)
        {
            this.conn = conn;
        }

        public IList<TableDefinition> GetTableDefinitions()
        {
            var cmd = conn.CreateCommand();
            var tables = conn.Query<TableDefinition>(
                @"
select
	s.schema_id as [SchemaId],
	s.name as [SchemaName],
	o.object_id as [TableId],
	o.name as [TableName]
from sys.objects o
join sys.schemas s on s.schema_id = o.schema_id
where o.type = 'U'").ToList();

            foreach (var tableDef in tables)
            {
                tableDef.Columns = conn.Query<ColumnDefinition>(@"
select
	c.object_id as [TableId],
	c.name as [ColumnName],
	c.column_id as [ColumnId],
	c.system_type_id as [SystemTypeId],
	c.max_length as [MaxLength],
	c.is_nullable as [IsNullable]
from sys.columns c
where c.object_id = @TableId",
                     new { TableId = tableDef.TableId }).ToList();
            }

            return tables;
        }
    }
}
