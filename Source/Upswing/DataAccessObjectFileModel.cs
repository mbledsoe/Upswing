using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Upswing
{
    public class DataAccessObjectFileModel
    {
        public string EntityName { get; set; }
        public ColumnDefinition IdColumn { get; set; }
        public string InsertSql { get; set; }
        public string UpdateSql { get; set; }
        public string SelectSql { get; set; }
        public string DeleteSql { get; set; }

        public IList<DataAccessObjectFileModelSqlParameter> InsertParameters { get; set; }
        public IList<DataAccessObjectFileModelSqlParameter> UpdateParameters { get; set; }
        public string Namespace { get; internal set; }
        public IList<EntityFileProperty> Properties { get; internal set; }
    }
}
