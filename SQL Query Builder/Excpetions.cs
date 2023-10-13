using SQL_Query_Builder.Interfaces;

namespace SQL_Query_Builder
{
    public class SQLQueryBuilderException : Exception, ISQLQueryBuilderException
    {
        public SQLQueryBuilderException(string message) : base(message) { }

        public static SQLQueryBuilderException DatabaseConnectionException(string message) => new(message);
        public static SQLQueryBuilderException EmptyId => new("Id is empty");
        public static SQLQueryBuilderException InvalidQuery => new("InvalidQuery");
        public static SQLQueryBuilderException InvalidClass => new("Couldn't instantiate class");
    }
}
