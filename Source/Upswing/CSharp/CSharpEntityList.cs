using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing.CSharp
{
    public class CSharpEntityList : ITemplateModel
    {
        public IList<CSharpEntity> Entities { get; set; } = new List<CSharpEntity>();
        public string Name { get => "DapperStartup"; }

        public string Namespace { get; set; }
    }
}
