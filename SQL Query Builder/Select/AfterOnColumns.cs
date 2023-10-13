using SQL_Query_Builder.Interfaces;

namespace SQL_Query_Builder.Select
{
    public class AfterOnColumns : AfterSelect
    {
        public AfterOnColumns(ICommand command) : base(command) { }
        public AfterOnColumns OrOnColumns(string column1, string table1, string column2, string table2)
        {
            command.AddTextToCommand($"OR {table1}.{column1} = {table2}.{column2}");
            return new AfterOnColumns(command);
        }

    }
}
