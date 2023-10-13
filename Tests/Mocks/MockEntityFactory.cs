using SQL_Query_Builder.Interfaces;

namespace Unit_testing.Mocks.SQL
{
    public class MockEntityFactory : IEntityFactory
    {
        public T Create<T>(IDbDataReader reader)
        {
            return (T)(object)new MockEntity();
        }
    }
}
