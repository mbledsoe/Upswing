namespace Upswing
{
    public class MatchAllTableSpec : ITableSpec
    {
        public bool IsMatch(TableDefinition tableDef)
        {
            return true;
        }
    }
}