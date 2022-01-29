namespace Upswing
{
    public class EntityFileProperty
    {
        public string TypeName { get; set; }
        public string Name { get; set; }
        public bool IsNullable { get; set; }        
        public ColumnDefinition Column { get; set; }
    }
}
