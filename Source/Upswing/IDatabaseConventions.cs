namespace Upswing
{
    public interface IDatabaseConventions
    {
        ColumnDefinition GetIdColumn(TableDefinition tableDef);
    }
}
