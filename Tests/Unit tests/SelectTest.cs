using SQL_Query_Builder;
using SQL_Query_Builder.Select;
using Unit_testing.Mocks.SQL;

namespace Unit_testing.Tests.SQLQueryBuilder
{
    [TestClass]
    public class SelectTest : Base
    {
        private Select select
        {
            get
            {
                Reset();
                return new(builderCommandMock);
            }
        }
        [TestMethod]
        public void Join()
        {
            string secondTable = "second_table";
            var query = "SELECT * FROM " + stringCommandMock.TableName + " RIGHT JOIN " + secondTable + " ON " + stringCommandMock.TableName + ".id = " + secondTable + ".id";

            select
                .All
                .Join(secondTable, JoinType.Right)
                .OnColumns("id", "id")
                .Get<MockEntity>();

            AssertQuery(query);
        }

        [TestMethod]
        public void Where()
        {
            var query = "SELECT * FROM "
                + stringCommandMock.TableName
                + " WHERE id = @" + stringCommandMock.GetParamName()
                + " AND date >= @" + stringCommandMock.GetParamName()
                + " OR number < @" + stringCommandMock.GetParamName();

            select
                .All
                .Where("id")
                .Equals(1)
                .And
                .Where("date")
                .IsMoreOrEqual(new DateTime(2023, 1, 1))
                .Or
                .Where("number")
                .IsLess(4)
                .FinishSelect
                .Get<MockEntity>();

            AssertQuery(query);
        }
        [TestMethod]
        public void OnColumns()
        {
            var query = "SELECT id, name, email FROM " + stringCommandMock.TableName;

            select
                .OnlyColumns("id", "name", "email")
                .First<MockEntity>();

            AssertQuery(query);
        }

        [TestMethod]
        public void CountColumns()
        {
            var query = "SELECT COUNT(id, name, email) FROM " + stringCommandMock.TableName;

            int count = select
                .OnlyColumns("id", "name", "email")
                .Count;

            AssertQuery(query);
        }
        [TestMethod]
        public void CountAll()
        {
            var query = "SELECT COUNT(*) FROM " + stringCommandMock.TableName;

            int count = select
                .All
                .Count;

            AssertQuery(query);
        }
        [TestMethod]
        public void OrderBy()
        {
            var query = "SELECT * FROM " + stringCommandMock.TableName + " ORDER BY " + MockEntityTable.Id + " DESC";

            select
                .All
                .OrderBy(MockEntityTable.Id, false);

            AssertQuery(query);
        }
        [TestMethod]
        public void Distinct()
        {
            var query = "SELECT DISTINCT * FROM " + MockEntityTable.TableName;

            select.Distinct.All.Get<MockEntity>();

            AssertQuery(query);
        }
        [TestMethod]
        public void MustNotExecute()
        {
            Assert.ThrowsException<SQLQueryBuilderException>(() => select
                .All
                .Where(MockEntityTable.Id)
                .Equals(null)
                .Execute()
            );
        }
    }
}
