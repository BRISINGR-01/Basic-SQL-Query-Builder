using SQL_Query_Builder.Interfaces;

namespace SQL_Query_Builder.Where
{
    public class Or
    {
        private readonly ICommand command;
        public Or(ICommand command)
        {
            this.command = command;
            command.AddTextToCommand("OR");
        }
        public Where Where(string column) => new(column, command);
    }
}
