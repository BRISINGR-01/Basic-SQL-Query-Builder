using SQL_Query_Builder.Interfaces;

namespace SQL_Query_Builder.Select
{
    public class Select
    {
        private readonly ICommand command;
        public Select(ICommand command)
        {
            this.command = command;
            command.AddTextToCommand("SELECT");
        }

        public Select Distinct
        {
            get
            {
                command.AddTextToCommand("DISTINCT");
                return this;
            }
        }
        public AfterSelect All
        {
            get
            {
                command.AddTextToCommand($"* FROM {command.TableName}");
                return new AfterSelect(command);
            }
        }

        public AfterSelect OnlyColumns(params string[] columns)
        {
            command.AddTextToCommand($"{string.Join(", ", columns)} FROM {command.TableName}");
            return new AfterSelect(command);
        }
    }
}
