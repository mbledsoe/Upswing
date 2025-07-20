using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Upswing.SqlServer
{
	public class SqlServerMetadataService
	{
		private readonly SqlConnectionFactory sqlConnectionFactory;

		public SqlServerMetadataService(SqlConnectionFactory sqlConnectionFactory)
		{
			this.sqlConnectionFactory = sqlConnectionFactory;
		}

		public Task<DatabaseMetadata> GetMetadata()
		{
			using (var conn = sqlConnectionFactory.CreateConnection())
			{
				var tables = GetTables(conn);			

				foreach (var table in tables)
				{
					table.Columns = GetColumns(conn, table);
				}

				var metaData = new DatabaseMetadata(tables);

				return Task.FromResult(metaData);
			}				
		}

		private IReadOnlyCollection<SqlTable> GetTables(SqlConnection conn)
		{
			var command = conn.CreateCommand();
			command.CommandText = @"
select
	st.name,
	st.object_id,
	ss.schema_id,
	ss.name as [schema_name]
from sys.tables st
join sys.schemas ss on ss.schema_id = st.schema_id
where st.type = 'U'
order by ss.name, st.name";

			var results = new List<SqlTable>();

			using (var rdr = command.ExecuteReader())
			{
				while (rdr.Read())
				{
					var tableMetadata = new SqlTable
					{
						ObjectId = (int)rdr["object_id"],
						Name = (string)rdr["name"],
						SchemaId = (int)rdr["schema_id"],
						SchemaName = (string)rdr["schema_name"]
					};

					results.Add(tableMetadata);
				}
			}

			return results;
		}

		private IList<SqlColumn> GetColumns(SqlConnection conn, SqlTable table)
		{
			var command = conn.CreateCommand();
			command.CommandText = @"
select
	c.object_id,
	c.name,
	c.column_id,
	c.system_type_id,
	c.user_type_id,
	c.max_length,
	c.precision,
	c.scale,
	c.is_nullable,
	c.is_identity
from sys.columns c
where object_id = @object_id";
			command.Parameters.AddWithValue("object_id", table.ObjectId);

			var results = new List<SqlColumn>();

			using (var rdr = command.ExecuteReader())
			{
				while (rdr.Read())
				{
					var tableMetadata = new SqlColumn
					{
						ObjectId = (int)rdr["object_id"],
						Name = (string)rdr["name"],
						ColumnId = (int)rdr["column_id"],
						SystemTypeId = (byte)rdr["system_type_id"],
						UserTypeId = (int)rdr["user_type_id"],
						MaxLength = (short)rdr["max_length"],
						Precision = (byte)rdr["Precision"],
						Scale = (byte)rdr["Scale"],
						IsNullable = (bool)rdr["is_nullable"],
						IsIdentity = (bool)rdr["is_identity"]						
					};

					results.Add(tableMetadata);
				}
			}

			return results;
		}
	}
}
