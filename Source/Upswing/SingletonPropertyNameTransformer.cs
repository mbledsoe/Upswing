using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing
{
    class SingletonPropertyNameTransformer : IPropertyNameTransformer
    {
        /* Add an optional parameter to identify special characters in column name that should be removed.
         * Add an optional parameter to remove prefix, suffix, or both special character for example: _,$,@ */
        public string TransformName(ColumnDefinition column)
        {
            // Fine tune this logic once filter is implemented.
            var columnName = $"{column.ColumnName}";
            int index = columnName.LastIndexOf("_");
            if (index >= 0)
            {                
                // Check if column is an IDNO field type.  If so Append [Id] to class property name.
                if (columnName.EndsWith("_IDNO"))
                    columnName = columnName[..index] + "_Id";
                else
                    columnName = columnName[..index];

                columnName = ConvertToPascal(columnName, '_');             
            }

            return columnName;         
        }

        public static string ConvertToPascal(string columnName, char columnSeperator)
        {
            string[] words = columnName.Split(columnSeperator);

            StringBuilder returnStr = new StringBuilder();

            foreach (string wrd in words)
            {
                returnStr.Append(wrd.Substring(0, 1).ToUpper());
                returnStr.Append(wrd.Substring(1).ToLower());

            }
            return returnStr.ToString();
        }
    }
}
