using SQL_Query_Builder.Interfaces;

namespace Unit_testing.Mocks.SQL
{
    public class MockEntity
    {
        public string String { get; private set; }
        public string? NullableString { get; private set; }
        public Guid Id { get; private set; }
        public double Double { get; private set; }
        public double? NullableDouble { get; private set; }
        public DateTime DateTime { get; private set; }
        public DateTime? NullableDateTime { get; private set; }
        public int Int32 { get; private set; }
        public int? NullableInt32 { get; private set; }

        public MockEntity(string @string, string? nullableString, Guid id, double @double, double? nullableDouble, DateTime dateTime, DateTime? nullableDateTime, int int32, int? nullableInt32)
        {
            String = @string;
            NullableString = nullableString;
            Id = id;
            Double = @double;
            NullableDouble = nullableDouble;
            DateTime = dateTime;
            NullableDateTime = nullableDateTime;
            Int32 = int32;
            NullableInt32 = nullableInt32;
        }
        public MockEntity()
        {
            String = string.Empty;
            NullableString = null;
            Id = Guid.Empty;
            Double = 0;
            NullableDouble = null;
            DateTime = DateTime.Now;
            NullableDateTime = null;
            Int32 = 0;
            NullableInt32 = null;
        }
        public MockEntity FromReader(IDbDataReader reader)
        {
            return new(
                @string: reader.GetString(string.Empty),
                nullableString: reader.GetNullableString(string.Empty),
                id: reader.GetId(string.Empty),
                @double: reader.GetDouble(string.Empty),
                nullableDouble: reader.GetNullableDouble(string.Empty),
                dateTime: reader.GetDateTime(string.Empty),
                nullableDateTime: reader.GetNullableDateTime(string.Empty),
                int32: reader.GetInt32(string.Empty),
                nullableInt32: reader.GetNullableInt32(string.Empty)
            );
        }
    }
}
