using SQL_Query_Builder.Interfaces;

namespace SQL_Query_Builder.Select
{
    public class Join
    {
        private readonly ICommand command;
        private readonly string table2;
        public Join(string table, JoinType type, ICommand command)
        {
            table2 = table;
            this.command = command;
            command.AddTextToCommand($"{type.ToString().ToUpper()} JOIN {table2}");
        }

        public AfterOnColumns OnColumns(string column1, string column2)
        {
            command.AddTextToCommand($"ON {command.TableName}.{column1} = {table2}.{column2}");
            return new(command);
        }
    }
}
