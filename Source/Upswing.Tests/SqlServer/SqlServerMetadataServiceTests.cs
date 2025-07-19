using Upswing.SqlServer;

namespace Upswing.Tests.SqlServer
{
	internal class SqlServerMetadataServiceTests
	{		
		[Test]		
		public async Task CanGetTableList()
		{
			var sqlServerMetadataService = new SqlServerMetadataService(GetSqlConnectionFactory());
			var metaData = await sqlServerMetadataService.GetMetadata();

			Assert.That(metaData, Is.Not.Null);
			Assert.That(metaData.Tables.Count, Is.EqualTo(3));
			Assert.That(metaData.Tables.Any(t => t.Name.Equals("Person")));
			Assert.That(metaData.Tables.Any(t => t.Name.Equals("Hobby")));
			Assert.That(metaData.Tables.Any(t => t.Name.Equals("PersonHobby")));
		}

		private SqlConnectionFactory GetSqlConnectionFactory()
		{
			return new SqlConnectionFactory("server=localhost;database=UpswingDev;integrated security=true;TrustServerCertificate=true");
		}
	}
}
