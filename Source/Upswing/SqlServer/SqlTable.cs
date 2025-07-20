using System.Data;

namespace Upswing.SqlServer
{
	public class SqlTable
	{
		public required int ObjectId { get; set; }
		public required string Name { get; set; }
		public required int SchemaId { get; set; }
		public required string SchemaName { get; set; }

		public IList<SqlColumn> Columns { get; set; } = new List<SqlColumn>();
	}
}
