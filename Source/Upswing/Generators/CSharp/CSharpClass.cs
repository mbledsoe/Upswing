namespace Upswing.Generators.CSharp
{
	public class CSharpClass
	{
		public required string Name { get; set; }
		public required IList<CSharpProperty> Properties { get; set; }
	}
}
