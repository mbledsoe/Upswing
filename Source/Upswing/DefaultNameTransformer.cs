namespace Upswing
{
    public class DefaultNameTransformer : INameTransformer
    {
        public string TransformColumnName(ColumnDefinition column)
        {
            return column.ColumnName;
        }

        public string TransformTableName(TableDefinition tableDef)
        {
            return tableDef.TableName;
        }
    }
}
