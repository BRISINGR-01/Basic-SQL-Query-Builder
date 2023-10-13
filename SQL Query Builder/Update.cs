using SQL_Query_Builder.Interfaces;

namespace SQL_Query_Builder
{
    public class Update
    {
        private readonly ICommand command;
        private readonly List<string> columns = new();
        public Update(ICommand command)
        {
            this.command = command;
            command.AddTextToCommand($"UPDATE {command.TableName} SET");
        }
        public Update Set(string column, object? value)
        {
            columns.Add(column);
            command.SetParam(column, value);
            return this;
        }
        public Where.Where Where(string column)
        {
            command.AddTextToCommand(string.Join(", ", columns.Select((column) => $"{column} = @{column}")));
            command.AddTextToCommand("WHERE");
            return new Where.Where(column, command);
        }
    }
}
