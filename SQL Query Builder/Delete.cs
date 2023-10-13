using SQL_Query_Builder.Interfaces;

namespace SQL_Query_Builder
{
    public class Delete
    {
        private readonly ICommand command;
        public Delete(ICommand command)
        {
            this.command = command;
            command.AddTextToCommand($"DELETE FROM {command.TableName}");
        }
        public Where.Where Where(string column)
        {
            command.AddTextToCommand("WHERE");
            return new Where.Where(column, command);
        }
    }
}
