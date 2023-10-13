using SQL_Query_Builder.Interfaces;

namespace SQL_Query_Builder.Select
{
    public class AfterSelect : SelectFinish
    {
        public AfterSelect(ICommand ctx) : base(ctx) { }
        public Where.Where Where(string column)
        {
            command.AddTextToCommand("WHERE");

            return new(column, command);
        }
        public Join Join(string tableName, JoinType type = JoinType.Inner) => new(tableName, type, command);
    }
}
