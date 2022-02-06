using NUnit.Framework;

namespace Upswing.Tests
{
    [TestFixture]
    public class SingletonNameTransformerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("Applications_T1", "Applications")]
        [TestCase("Users_T1", "Users")]
        [TestCase("Issues_T1", "Issues")]
        [TestCase("Members_T", "Members")]
        public void CanTransformTableStandardizedNames(string tableName, string entityName)
        {
            var nameTransformer = new SingletonNameTransformer();
            var tableDef = new TableDefinition { TableName = tableName };

            Assert.AreEqual(entityName, nameTransformer.TransformTableName(tableDef));
        }

        [TestCase("Applications_X1", "Applications_X1")]
        [TestCase("Users_ABC", "Users_ABC")]
        [TestCase("Issues_1", "Issues_1")]
        [TestCase("Members_TFF", "Members_TFF")]
        public void DoesNotTransformNonStandardNames(string tableName, string entityName)
        {
            var nameTransformer = new SingletonNameTransformer();
            var tableDef = new TableDefinition { TableName = tableName };

            Assert.AreEqual(entityName, nameTransformer.TransformTableName(tableDef));
        }
    }
}