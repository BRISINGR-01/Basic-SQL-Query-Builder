using SQL_Query_Builder.Interfaces;
using System.Text.RegularExpressions;

namespace SQL_Query_Builder
{
    public class Insert
    {
        private readonly List<string> columns = new();
        private readonly ICommand command;
        public Insert(ICommand command)
        {
            this.command = command;
        }

        public Insert Set(string column, object? value)
        {
            columns.Add(column);
            command.SetParam(column, value);

            return this;
        }

        public bool Execute()
        {
            command.ReplaceCommand($"INSERT INTO {command.TableName} ({string.Join(", ", columns)}) VALUES ({string.Join(", ", columns.Select((column) => $"@{column}"))})");
            command.ReplaceCommand(Regex.Replace(command.CommandText, @",$", ")"));
            return command.ExecuteNonQuery();
        }
    }
}
