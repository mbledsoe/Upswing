using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing.Dapper
{
    public class DapperDaoModel : ITemplateModel
    {
        public string Name { get; set; }
        public string EntityName { get; set; }
        public string SelectAllSql { get; set; }
        public string SelectByIdSql { get; set; }
        public string InsertSql { get; set; }
        public string UpdateSql { get; set; }
        public string DeleteSql { get; set; }
        public string Namespace { get; set; }
        public string IdentityColumnName { get; set; }
        public string IdentityPropertyName { get; set; }
    }
}
