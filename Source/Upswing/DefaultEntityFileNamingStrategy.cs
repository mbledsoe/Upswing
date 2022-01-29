using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing
{
    public class DefaultEntityFileNamingStrategy : IEntityFileNamingStrategy
    {
        private readonly string formatString;

        public DefaultEntityFileNamingStrategy()
            :this("{0}.generated.cs")
        {
        }

        public DefaultEntityFileNamingStrategy(string formatString)
        {
            this.formatString = formatString;
        }

        public string GetFileName(EntityFileModel entityFileModel)
        {
            return string.Format(formatString, entityFileModel.EntityName);
        }
    }
}
