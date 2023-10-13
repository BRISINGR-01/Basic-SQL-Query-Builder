namespace SQL_Query_Builder.Interfaces
{
    public interface ICommand
    {
        public string TableName { get; }
        public string CommandText { get; }
        public IEntityFactory EntityFactory { get; }
        public void AddTextToCommand(string text);

        public bool CommandContains(string text);

        public void ReplaceCommand(string text);

        public string SetParamAndReturnName(object? value);
        public void SetParam(string param, object? value);
        public bool ExecuteNonQuery();
        public int ExecuteScalar();

        public List<T> GetAllValues<T>();
        public T? GetFirstValue<T>();
    }
}
