using System.Collections.Generic;

namespace Upswing
{
    public class TableDefinition
    {
        public int SchemaId { get; set; }
        public string SchemaName { get; set; }
        public int TableId { get; set; }
        public string TableName { get; set; }
        public IList<ColumnDefinition> Columns { get; set; }
    }
}