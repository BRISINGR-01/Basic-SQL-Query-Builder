using SQL_Query_Builder.Interfaces;

namespace Unit_testing.Mocks.SQL
{
    public class SqlReaderWrapperMock : IDbDataReader
    {
        public string GetString(string key) => string.Empty;
        public string? GetNullableString(string key) => GetString(key);
        public Guid GetId(string key) => Guid.NewGuid();
        public double GetDouble(string key) => 0.0;
        public double? GetNullableDouble(string key) => GetDouble(key);
        public DateTime GetDateTime(string key) => DateTime.Now;
        public DateTime? GetNullableDateTime(string key) => GetDateTime(key);
        public int GetInt32(string key) => 0;
        public int? GetNullableInt32(string key) => GetInt32(key);
    }
}
