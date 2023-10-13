using SQL_Query_Builder.Interfaces;
using SQL_Query_Builder.Select;

namespace SQL_Query_Builder.Where
{
    public class AfterWhere
    {
        private readonly ICommand command;
        public AfterWhere(ICommand command)
        {
            this.command = command;
        }
        public And And => new(command);
        public Or Or => new(command);
        public SelectFinish FinishSelect
        {
            get
            {
                if (!command.CommandContains("SELECT")) throw SQLQueryBuilderException.InvalidQuery;

                return new SelectFinish(command);
            }
        }
        public bool Execute()
        {
            if (command.CommandContains("SELECT")) throw SQLQueryBuilderException.InvalidQuery;

            return command.ExecuteNonQuery();
        }
    }
}
