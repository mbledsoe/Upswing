namespace Upswing.SqlServer
{
	public class DatabaseMetadata
	{
		public DatabaseMetadata(IReadOnlyCollection<SqlTable> tables)
		{
			Tables = tables;
		}

		public IReadOnlyCollection<SqlTable> Tables { get; }
	}
}
