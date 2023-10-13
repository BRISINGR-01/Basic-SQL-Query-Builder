using Unit_testing.Mocks.SQL;

namespace Unit_testing.Tests.SQLQueryBuilder
{
    [TestClass]
    public abstract class Base
    {
        protected CommandMock stringCommandMock { get; set; } = new();
        protected CommandMock builderCommandMock { get; set; } = new();
        protected void Reset()
        {
            stringCommandMock = new();
            builderCommandMock = new();
        }
        [TestMethod]
        protected void AssertQuery(string expected)
        {
            Assert.AreEqual(builderCommandMock.CommandText, expected);
        }
    }
}
