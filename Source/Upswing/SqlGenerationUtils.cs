﻿using System;

namespace Upswing
{
    internal static class SqlGenerationUtils
    {
        internal static string GetSqlDbType(short systemTypeId)
        {
            switch (systemTypeId)
            {
                case 56:
                    return "SqlDbType.Int";
                case 61:
                    return "SqlDbType.DateTime";
                case 104:
                    return "SqlDbType.Bit";
                case 231:
                    return "SqlDbType.NVarChar";
                default:
                    throw new Exception($"Unknown Column Type: {systemTypeId}");
            }
        }

        internal static string GetClrTypeName(ColumnDefinition column)
        {
            switch (column.SystemTypeId)
            {
                case 56:
                    return column.IsNullable ? "int?" : "int";
                case 61:
                    return column.IsNullable ? "DateTime?" : "DateTime";
                case 104:
                    return column.IsNullable ? "bool?" : "bool";
                case 231:
                    return "string";
                default:
                    throw new Exception($"Unknown Column Type: {column.SystemTypeId}");
            }
        }        
    }
}