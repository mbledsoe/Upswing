using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing.CSharp
{
    public class CSharpEntity : ITemplateModel
    {
        public string Namespace { get; set; }
        public string Name { get; set; }
        public IList<CSharpEntityProperty> Properties { get; set; }
        public TableDefinition SourceTable { get; set; }
    }
}
