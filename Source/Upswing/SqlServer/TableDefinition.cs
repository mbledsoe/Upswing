using System.Collections.Generic;
using System.Linq;

namespace Upswing
{
    public class TableDefinition
    {
        public int SchemaId { get; set; }
        public string SchemaName { get; set; }
        public int TableId { get; set; }
        public string TableName { get; set; }
        public IList<ColumnDefinition> Columns { get; set; }

        public bool HasIdentity => Columns.Any(c => c.IsIdentity);

        public ColumnDefinition IdentityColumn => Columns.Single(c => c.IsIdentity);

        public IEnumerable<ColumnDefinition> MutableColumns => Columns.Where(c => !c.IsIdentity && !c.IsComputed);
    }
}