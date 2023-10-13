using SQL_Query_Builder.Interfaces;

namespace SQL_Query_Builder.Where
{
    public class And
    {
        private readonly ICommand command;
        public And(ICommand command)
        {
            this.command = command;
            command.AddTextToCommand("AND");
        }
        public Where Where(string column) => new(column, command);
    }
}
