using Upswing.SqlServer;

namespace Upswing.Generators.CSharp
{
	internal class CSharpTypeMap
	{
		internal static string MapFromSqlSystemTypeId(int systemTypeId)
		{
			switch (systemTypeId)
			{
				case 36:
					return "Guid";
				case 231:
					return "string";
				case 40:
					return "DateTime";
				case 61:
					return "DateTimeOffset";
				default:
					throw new Exception($"Unable to map SQL Server Type {systemTypeId} to CSharp Type.");
			}
		}
	}
}
