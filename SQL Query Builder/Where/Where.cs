using SQL_Query_Builder.Interfaces;

namespace SQL_Query_Builder.Where
{
    public class Where
    {
        private readonly ICommand command;
        private readonly string column;
        public Where(string column, ICommand command)
        {
            this.column = column;
            this.command = command;
        }

        public new AfterWhere Equals(object? val)
        {
            string paramName = command.SetParamAndReturnName(val);

            command.AddTextToCommand($"{column} = @{paramName}");

            return new AfterWhere(command);
        }
        public AfterWhere Contains(string val)
        {
            string paramName = command.SetParamAndReturnName(val);

            command.AddTextToCommand($"{column} LIKE %@{paramName}%");
            return new AfterWhere(command);
        }
        public AfterWhere IsLess(object val)
        {
            string paramName = command.SetParamAndReturnName(val);

            command.AddTextToCommand($"{column} < @{paramName}");
            return new AfterWhere(command);
        }
        public AfterWhere IsLessOrEqual(object val)
        {
            string paramName = command.SetParamAndReturnName(val);

            command.AddTextToCommand($"{column} <= @{paramName}");

            return new AfterWhere(command);
        }
        public AfterWhere IsMore(object val)
        {
            string paramName = command.SetParamAndReturnName(val);

            command.AddTextToCommand($"{column} > @{paramName}");

            return new AfterWhere(command);
        }
        public AfterWhere IsMoreOrEqual(object val)
        {
            string paramName = command.SetParamAndReturnName(val);

            command.AddTextToCommand($"{column} >= @{paramName}");

            return new AfterWhere(command);
        }
    }
}
