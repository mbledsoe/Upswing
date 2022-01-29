using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing
{
    public class DefaultEntityFileNamingStrategy : IEntityFileNamingStrategy
    {
        public string GetFileName(EntityFileModel entityFileModel)
        {
            return $"{entityFileModel.EntityName}.generated.cs";
        }
    }
}
