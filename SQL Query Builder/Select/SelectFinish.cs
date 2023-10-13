using SQL_Query_Builder.Interfaces;

namespace SQL_Query_Builder.Select
{
    public class SelectFinish
    {
        public readonly ICommand command;
        public SelectFinish(ICommand command)
        {
            this.command = command;
        }
        public SelectFinish OrderBy(string column, bool isAscending = true)
        {
            command.AddTextToCommand($"ORDER BY {column} {(isAscending ? "ASC" : "DESC")}");

            return this;
        }

        public T? First<T>() => command.GetFirstValue<T>();

        public List<T> Get<T>() => command.GetAllValues<T>();

        public int Count
        {
            get
            {
                var text = command.CommandText;
                text = text.Replace("SELECT ", "SELECT COUNT(");
                text = text.Replace(" FROM", ") FROM");

                command.ReplaceCommand(text);

                return command.ExecuteScalar();
            }
        }
    }
}
