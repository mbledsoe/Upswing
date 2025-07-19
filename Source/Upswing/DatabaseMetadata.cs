namespace Upswing
{
	public class DatabaseMetadata
	{
		public DatabaseMetadata(IReadOnlyCollection<TableMetadata> tables)
		{
			Tables = tables;
		}

		public IReadOnlyCollection<TableMetadata> Tables { get; }
	}
}
