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
    public class ScribanEntityTemplate : IEntityTemplate
    {
        private readonly ITemplateSource templateSource;

        public ScribanEntityTemplate(ITemplateSource templateSource)
        {
            this.templateSource = templateSource;
        }

        public string Render(EntityFileModel entityFileModel)
        {
            var template = templateSource.ReadTemplate();
            return template.Render(entityFileModel);
        }
    }
}
