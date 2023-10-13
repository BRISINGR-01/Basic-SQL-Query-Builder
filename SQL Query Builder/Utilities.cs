using SQL_Query_Builder.Interfaces;

namespace SQL_Query_Builder
{
    public class Utilities
    {
        public static string? ParseValue(object val, IEnumToStringConverter enumConverter)
        {
            string? value = null;
            if (val is Guid guid)
            {
                if (guid == Guid.Empty) throw SQLQueryBuilderException.EmptyId;

                return guid.ToString();
            }
            else if (val is DateTime date)
            {
                value = FormatDate(date);
            }
            else if (val is Enum @enum)
            {
                value = enumConverter.Convert(@enum);
            }
            else if (val is not string)
            {
                value = val.ToString();
            }
            else if (val is string @str)
            {
                value = @str;
            }

            return value;
        }
        public static string FormatDate(DateTime date) => date.ToString("yyyy-MM-ddTHH:mm:ss");
    }
}
