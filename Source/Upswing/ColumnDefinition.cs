namespace Upswing
{
    public class ColumnDefinition
    {
        public int TableId { get; set; }
        public string ColumnName { get; set; }
        public int ColumnId { get; set; }
        public short SystemTypeId { get; set; }
        public short MaxLength { get; set; }
        public bool IsNullable { get; set; }
    }
}