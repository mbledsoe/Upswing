namespace Upswing.SqlServer
{
	public class SqlColumn
	{
		public required int ObjectId { get; set; }
		public required string Name { get; set; }
		public required int ColumnId { get; set; }
		public required byte SystemTypeId { get; set; }
		public required int UserTypeId { get; set; }
		public required short MaxLength { get; set; }
		public required byte Precision { get; set; }
		public required byte Scale { get; set; }
		public required bool IsNullable { get; set; }
		public required bool IsIdentity { get; set; }
	}
}
