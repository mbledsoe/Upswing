namespace Upswing.CSharp
{
    public class CSharpEntityProperty
    {
        public string ClrType { get; set; }
        public string Name { get; set; }
        public bool IsNullable { get; set; }
        public ColumnDefinition SourceColumn { get; set; }
    }
}
