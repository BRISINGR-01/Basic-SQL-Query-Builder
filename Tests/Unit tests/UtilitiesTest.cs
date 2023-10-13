using SQL_Query_Builder;
using Unit_testing.Mocks.SQL;

namespace Unit_testing.Tests.SQLQueryBuilder
{
    [TestClass]
    public class UtilitiesTest
    {
        [TestMethod]
        public void FormatDate()
        {
            string formatted = "2023-05-12T18:35:00";
            var date = new DateTime(2023, 5, 12, 18, 35, 00);

            Assert.AreEqual(formatted, Utilities.FormatDate(date));
        }
        [TestMethod]
        public void ParseValue()
        {
            var id = Guid.NewGuid();
            Assert.AreEqual(id.ToString(), Parse(id));

            var date = DateTime.Now;
            Assert.AreEqual(Utilities.FormatDate(date), Parse(date));

            Assert.AreEqual(new EnumConverterMock().Convert(MockEnum.None), Parse(MockEnum.None));

            Assert.AreEqual("string", Parse(new MockToString()));
            Assert.AreEqual("1", Parse(1));
            Assert.AreEqual("some str", Parse("some str"));
        }
        private string? Parse(object val) => Utilities.ParseValue(val, new EnumConverterMock());
    }

    internal enum MockEnum { None }
    internal class MockToString
    {
        public override string ToString()
        {
            return "string";
        }
    }
}
