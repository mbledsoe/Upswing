namespace Upswing
{
    public class ColumnDefinition
    {
        public int TableId { get; set; }
        public string ColumnName { get; set; }
        public int ColumnId { get; set; }
        public short SystemTypeId { get; set; }
        public short MaxLength { get; set; }
        public byte Precision { get; set; }
        public byte Scale { get; set; }
        public bool IsRowGuidCol { get; set; }
        public bool IsNullable { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsComputed { get; set; }
    }
}