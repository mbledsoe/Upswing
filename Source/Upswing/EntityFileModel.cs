using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing
{
    public class EntityFileModel
    {
        public string Namespace { get; set; }
        public string EntityName { get; set; }
        public IList<EntityFileProperty> Properties { get; set; }
    }
}
