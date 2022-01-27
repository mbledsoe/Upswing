namespace Upswing
{
    public interface ITableSpec
    {
        bool IsMatch(TableDefinition tableDef);
    }
}