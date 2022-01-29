using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Scriban;

namespace Upswing.Scriban
{
    public class ScribanTemplate<T> : ITemplate<T>
    {
        private readonly ITemplateSource templateSource;

        public ScribanTemplate(ITemplateSource templateSource)
        {
            this.templateSource = templateSource;
        }

        public string Render(T templateFileModel)
        {
            var template = templateSource.ReadTemplate();
            return template.Render(templateFileModel);
        }
    }
}
