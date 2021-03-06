using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Upswing
{
    public class SingletonNameTransformer : INameTransformer
    {
        /* Add an optional parameter to identify special characters in column name that should be removed.
         * Add an optional parameter to remove prefix, suffix, or both special character for example: _,$,@ */
        public string TransformColumnName(ColumnDefinition column)
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

        public string TransformTableName(TableDefinition tableDef)
        {
            var match = Regex.Match(tableDef.TableName, @"^(.*)_T[0-9]*$");
            
            if (match.Success)
            {
                if (match.Groups.Count >= 2)
                {
                    return match.Groups[1].Value;
                }
            }

            return tableDef.TableName;
        }
    }
}
