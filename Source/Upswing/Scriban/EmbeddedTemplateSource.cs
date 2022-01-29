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
    public class EmbeddedTemplateSource : ITemplateSource
    {
        private readonly string templateName;

        public EmbeddedTemplateSource(string templateName)
        {
            this.templateName = templateName;
        }

        public Template ReadTemplate()
        {
            using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(templateName))
            {
                using (var streamReader = new StreamReader(resourceStream))
                {
                    var templateSource = streamReader.ReadToEnd();
                    return Template.Parse(templateSource);
                }
            }
        }
    }
}
