namespace Upswing
{
    internal class MatchAllTableSpec : ITableSpec
    {
        public bool IsMatch(TableDefinition tableDef)
        {
            return true;
        }
    }
}