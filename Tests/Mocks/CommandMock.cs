using SQL_Query_Builder.Interfaces;

namespace Unit_testing.Mocks.SQL
{
    public class CommandMock : ICommand
    {
        public string TableName => MockEntityTable.TableName;
        public string CommandText { get; private set; }
        public IEntityFactory EntityFactory { get; private set; }
        private int paramCount;
        public CommandMock()
        {
            EntityFactory = new MockEntityFactory();
            CommandText = string.Empty;
            paramCount = 0;
        }
        public void AddTextToCommand(string text)
        {
            if (string.IsNullOrEmpty(CommandText))
            {
                CommandText = text;
            }
            else
            {
                CommandText += " " + text;
            }
        }
        public bool CommandContains(string text) => CommandText.Contains(text);
        public void ReplaceCommand(string text)
        {
            CommandText = text;
        }
        public string SetParamAndReturnName(object? value) => GetParamName();
        public string GetParamName()
        {
            paramCount++;
            return "param" + paramCount;
        }
        public void SetParam(string param, object? value) { }
        public bool ExecuteNonQuery() => true;
        public int ExecuteScalar() => 0;
        public List<T> GetAllValues<T>() => new() { EntityFactory.Create<T>(new SqlReaderWrapperMock()), EntityFactory.Create<T>(new SqlReaderWrapperMock()) };
        public T? GetFirstValue<T>() => EntityFactory.Create<T>(new SqlReaderWrapperMock());
        public void Close() { }
    }
}
