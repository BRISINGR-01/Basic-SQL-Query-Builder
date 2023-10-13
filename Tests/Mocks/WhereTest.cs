using SQL_Query_Builder.Where;
using Unit_testing.Tests.SQLQueryBuilder;

namespace Unit_testing.Mocks.SQL
{
    [TestClass]
    public class WhereTest : Base
    {
        private Where where
        {
            get
            {
                Reset();
                return new(MockEntityTable.TableName, builderCommandMock);
            }
        }

        [TestMethod]
        public void Equals()
        {
            where.Equals(null);
            Assert.IsTrue(builderCommandMock.CommandContains("="));
        }
        [TestMethod]
        public void Contains()
        {
            where.Contains(string.Empty);
            Assert.IsTrue(builderCommandMock.CommandContains("LIKE"));
        }
        [TestMethod]
        public void IsLess()
        {
            where.IsLess(0);
            Assert.IsTrue(builderCommandMock.CommandContains("<"));
        }
        [TestMethod]
        public void IsLessOrEqual()
        {
            where.IsLessOrEqual(0);
            Assert.IsTrue(builderCommandMock.CommandContains("<="));
        }
        [TestMethod]
        public void IsMore()
        {
            where.IsMore(0);
            Assert.IsTrue(builderCommandMock.CommandContains(">"));
        }
        [TestMethod]
        public void IsMoreOrEqual()
        {
            where.IsMoreOrEqual(0);
            Assert.IsTrue(builderCommandMock.CommandContains(">="));
        }
        [TestMethod]
        public void And()
        {
            where.Equals(null).And.Equals(null);
            Assert.IsTrue(builderCommandMock.CommandContains("AND"));
        }
        [TestMethod]
        public void Or()
        {
            where.Equals(null).Or.Equals(null);
            Assert.IsTrue(builderCommandMock.CommandContains("OR"));
        }
    }
}
