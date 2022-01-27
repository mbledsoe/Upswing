using System.Collections.Generic;
using System.Linq;

namespace Upswing
{
    internal class MatchOnNameTableSpec : ITableSpec
    {
        private Dictionary<string, bool> includedTables = new Dictionary<string, bool>();

        public MatchOnNameTableSpec(IList<string> tables)
        {
            includedTables = tables.ToDictionary(table => table.ToLower(), table => true);
        }

        public bool IsMatch(TableDefinition tableDef)
        {
            return includedTables.ContainsKey(tableDef.TableName.ToLower());
        }
    }
}