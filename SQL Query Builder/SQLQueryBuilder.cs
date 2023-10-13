using MySql.Data.MySqlClient;
using SQL_Query_Builder.Interfaces;

namespace SQL_Query_Builder
{
    public class SQLQueryBuilder
    {
        private readonly MySqlConnection connection;
        private readonly string tableName;
        private readonly IEnumToStringConverter converter;
        private readonly IEntityFactory entityBuilder;
        private MySqlCommand command
        {
            get
            {
                MySqlCommand comm = new(string.Empty, connection);

                try
                {
                    comm.Connection.Open();
                }
                catch (MySqlException ex)
                {
                    throw ex.Number switch
                    {
                        0 => SQLQueryBuilderException.DatabaseConnectionException("Cannot connect to server"),
                        1042 => SQLQueryBuilderException.DatabaseConnectionException("Cannot connect to the database"),
                        1045 => SQLQueryBuilderException.DatabaseConnectionException("Invalid username/password provided"),
                        _ => SQLQueryBuilderException.DatabaseConnectionException("Something went wrong while connecting to the database: " + ex.Message),
                    };
                };

                return comm;
            }
        }
        public SQLQueryBuilder(string connectionString, string tableName, IEnumToStringConverter converter, IEntityFactory entityBuilder)
        {
            this.connection = new MySqlConnection(connectionString);
            this.tableName = tableName;
            this.converter = converter;
            this.entityBuilder = entityBuilder;
        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private SQLQueryBuilder(string connectionString)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            this.connection = new MySqlConnection(connectionString);
        }
        public SQLQueryBuilder FromTable(string tableName)
        {
            return new SQLQueryBuilder(connection.ConnectionString, tableName, converter, entityBuilder);
        }
        public static void Test(string connectionString)
        {
            var _ = new SQLQueryBuilder(connectionString).command;
        }
        private Command cmd => new(command, tableName, converter, entityBuilder);
        public Select.Select Select => new(cmd);
        public Insert Insert => new(cmd);
        public Update Update => new(cmd);
        public Delete Delete => new(cmd);
    }
}
