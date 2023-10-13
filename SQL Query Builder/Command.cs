using MySql.Data.MySqlClient;
using SQL_Query_Builder.Interfaces;

namespace SQL_Query_Builder
{
    public class Command : ICommand
    {
        private readonly MySqlCommand command;
        private readonly IEnumToStringConverter enumConverter;
        public IEntityFactory EntityFactory { get; private set; }
        public string CommandText => command.CommandText;
        public string TableName { get; private set; }

        public Command(MySqlCommand command, string tableName, IEnumToStringConverter converter, IEntityFactory entityBuilder)
        {
            TableName = tableName;
            enumConverter = converter;
            this.EntityFactory = entityBuilder;
            this.command = command;
        }

        public void AddTextToCommand(string text)
        {
            command.CommandText += $" {text}";
        }

        public bool CommandContains(string text)
        {
            return command.CommandText.Contains(text);
        }

        public void ReplaceCommand(string text)
        {
            command.CommandText = text;
        }

        private void CloseConnection()
        {
            command.Connection.Close();
            command.Connection.Dispose();
        }

        public string SetParamAndReturnName(object? value)
        {
            string paramName = "param" + command.Parameters.Count;
            SetParam(paramName, value);

            return paramName;
        }
        public void SetParam(string param, object? value)
        {
            if (value == null)
            {
                command.Parameters.AddWithValue($"@{param}", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue($"@{param}", Utilities.ParseValue(value, enumConverter));
            }
        }

        public bool ExecuteNonQuery()
        {

            bool result = false;
            try
            {
                command.Prepare();

                result = command.ExecuteNonQuery() > 0;
            }
            catch (MySqlException ex)
            {
                throw new SQLQueryBuilderException(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }
        public int ExecuteScalar()
        {

            int result;
            try
            {
                command.Prepare();

                result = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (MySqlException ex)
            {
                throw new SQLQueryBuilderException(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }
        public List<T> GetAllValues<T>()
        {
            command.Prepare();
            var reader = command.ExecuteReader();
            List<T> values = new();

            while (reader.Read())
            {
                values.Add(EntityFactory.Create<T>(new SqlReaderWrapper(reader)));
            }

            CloseConnection();

            return values;
        }
        public T? GetFirstValue<T>()
        {
            command.Prepare();

            var reader = command.ExecuteReader();

            T? val = default;

            if (reader.Read())
            {
                val = EntityFactory.Create<T>(new SqlReaderWrapper(reader));
            }

            CloseConnection();

            return val;
        }
    }
}
