using System.Linq;

namespace Upswing
{
    public class DefaultDatabaseConventions : IDatabaseConventions
    {
        public ColumnDefinition GetIdColumn(TableDefinition tableDef)
        {
            var idColumnName = $"{tableDef.TableName}Id";
            return tableDef.Columns.Single(c => string.Compare(c.ColumnName, idColumnName) == 0);
        }
    }
}
