using Microsoft.Data.SqlClient;

namespace Upswing.SqlServer
{
	public class SqlConnectionFactory
	{
		private readonly string connString;

		public SqlConnectionFactory(string connString)
		{
			this.connString = connString;
		}

		public SqlConnection CreateConnection()
		{
			var conn = new SqlConnection(connString);
			conn.Open();

			return conn;
		}
	}
}
