using Scriban;
using Upswing.SqlServer;

namespace Upswing.Generators.CSharp
{
	public class CSharpEntityGenerator
	{
		public string Generate(SqlTable table)
		{
			var templateModel = CreateTemplateModel(table);
			var template = GetTemplate();
			
			return template.Render(templateModel);
		}

		private Template GetTemplate()
		{
			return Template.Parse(File.ReadAllText(".\\Generators\\CSharp\\CSharpEntityGeneratorTemplate.scriban"));
		}

		private CSharpEntityGeneratorTemplateModel CreateTemplateModel(SqlTable table)
		{
			return new CSharpEntityGeneratorTemplateModel
			{
				Class = new CSharpClass
				{
					Name = table.Name,
					Properties = table.Columns.Select(c => new CSharpProperty
					{
						Name = c.Name,
						TypeName = CSharpTypeMap.MapFromSqlSystemTypeId(c.SystemTypeId)
					})
					.ToList()
				}
			};
		}
	}
}
