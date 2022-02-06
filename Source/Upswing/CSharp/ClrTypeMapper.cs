using System;

namespace Upswing.CSharp
{
    public class ClrTypeMapper
    {
        public static string GetClrType(ColumnDefinition column)
        {
            switch (column.SystemTypeId)
            {
                case 52:
                    return column.IsNullable ? "short?" : "short";
                case 56:
                    return column.IsNullable ? "int?" : "int";
                case 61:
                    return column.IsNullable ? "DateTime?" : "DateTime";
                case 104:
                    return column.IsNullable ? "bool?" : "bool";
                case 106:
                    return column.IsNullable ? "double?" : "double";
                case 127:
                    return column.IsNullable ? "long?" : "long";
                case 35:
                case 99:
                case 167:
                case 175:
                case 231:
                    return "string";
                default:
                    throw new Exception($"Unknown Column Type: {column.SystemTypeId}");
            }
        }
    }
}
