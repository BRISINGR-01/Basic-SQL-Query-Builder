namespace SQL_Query_Builder.Interfaces
{
    public interface IDbDataReader
    {
        public string GetString(string key);
        public string? GetNullableString(string key);
        public Guid GetId(string key);
        public double GetDouble(string key);
        public double? GetNullableDouble(string key);
        public DateTime GetDateTime(string key);
        public DateTime? GetNullableDateTime(string key);
        public int GetInt32(string key);
        public int? GetNullableInt32(string key);
    }
}
