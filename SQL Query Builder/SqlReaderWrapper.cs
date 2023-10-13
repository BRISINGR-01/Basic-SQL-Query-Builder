using MySql.Data.MySqlClient;
using SQL_Query_Builder.Interfaces;

namespace SQL_Query_Builder
{
    public class SqlReaderWrapper : IDbDataReader
    {
        private readonly MySqlDataReader reader;
        public SqlReaderWrapper(MySqlDataReader reader)
        {
            this.reader = reader;
        }

        public string GetString(string key) => reader.GetString(key);
        public string? GetNullableString(string key) => IsNull(key) ? null : GetString(key);
        public Guid GetId(string key) => Guid.Parse(GetString(key));
        private bool IsNull(string key) => reader.IsDBNull(reader.GetOrdinal(key));
        public double GetDouble(string key) => reader.GetDouble(key);
        public double? GetNullableDouble(string key) => IsNull(key) ? null : GetDouble(key);
        public DateTime GetDateTime(string key) => reader.GetDateTime(key);
        public DateTime? GetNullableDateTime(string key) => IsNull(key) ? null : GetDateTime(key);
        public int GetInt32(string key) => reader.GetInt32(key);
        public int? GetNullableInt32(string key) => IsNull(key) ? null : GetInt32(key);
    }
}
