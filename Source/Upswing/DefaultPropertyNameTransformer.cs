namespace Upswing
{
    public class DefaultPropertyNameTransformer : IPropertyNameTransformer
    {
        public string TransformName(ColumnDefinition column)
        {
            return column.ColumnName;
        }
    }
}
