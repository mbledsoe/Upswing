using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing
{
    class SingletonPropertyNameTransformer : IPropertyNameTransformer
    {
        public string TransformName(ColumnDefinition column)
        {
            return $"{column.ColumnName.ToUpper()}_L33T";
        }
    }
}
