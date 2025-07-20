using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upswing.Generators.CSharp;
using Upswing.SqlServer;

namespace Upswing.Tests.Generators.CSharp
{
	internal class CSharpEntityGeneratorTests
	{
		[Test]
		public void CanGenerateEntityFileContents()
		{
			var table = new SqlTable
			{
				ObjectId = 9876,
				Name = "Person",
				SchemaId = 0123,
				SchemaName = "dbo",
				Columns = new List<SqlColumn>
				{
					new SqlColumn
					{
						ObjectId = 9876,
						Name = "Id",
						ColumnId = 1,
						SystemTypeId = 36,
						UserTypeId = 36,
						MaxLength = 16,						
						Precision = 0,
						Scale = 0,
						IsNullable = false,
						IsIdentity = false
					},
					new SqlColumn
					{
						ObjectId = 9876,
						Name = "FirstName",
						ColumnId = 2,						
						SystemTypeId = 231,
						UserTypeId = 231,
						MaxLength = 100,
						Precision = 0,
						Scale = 0,
						IsNullable = false,
						IsIdentity = false
					},
					new SqlColumn					
					{
						ObjectId = 9876,
						Name = "LastName",
						ColumnId = 3,						
						SystemTypeId = 231,
						UserTypeId = 231,
						MaxLength = 100,
						Precision = 0,
						Scale = 0,
						IsNullable = false,
						IsIdentity = false
					},
					new SqlColumn
					{
						ObjectId = 9876,
						Name = "BirthDate",
						ColumnId = 4,
						SystemTypeId = 40,
						UserTypeId = 40,
						MaxLength = 3,
						Precision = 10,
						Scale = 0,
						IsNullable = false,
						IsIdentity = false
					},
					new SqlColumn
					{
						ObjectId = 9876,
						Name = "CreatedAt",
						ColumnId = 5,
						SystemTypeId = 61,
						UserTypeId = 61,
						MaxLength = 8,
						Precision = 23,
						Scale = 3,
						IsNullable = false,
						IsIdentity = false
					}
				}
			};

			var generator = new CSharpEntityGenerator();
			var output = generator.Generate(table);

			var expectedFileContents = File.ReadAllText(".\\Generators\\CSharp\\CSharpEntityGeneratorSampleOutput.txt");

			Assert.That(output, Is.EquivalentTo(expectedFileContents));
		}
	}
}
