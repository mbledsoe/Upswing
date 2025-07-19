using System.Collections.Generic;
using System.Threading.Tasks;
using Upswing;
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

				var results = new List<TableMetadata>();

				using (var rdr = command.ExecuteReader())
				{
					while (rdr.Read())
					{
						var tableMetadata = new TableMetadata
						{
							ObjectId = (int)rdr["object_id"],
							Name = (string)rdr["name"],
							SchemaId = (int)rdr["schema_id"],
							SchemaName = (string)rdr["schema_name"]
						};

						results.Add(tableMetadata);
					}
				}

				var metaData = new DatabaseMetadata(results);

				return Task.FromResult(metaData);
			}				
		}
	}
}
