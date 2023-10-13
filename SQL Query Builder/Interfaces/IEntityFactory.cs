namespace SQL_Query_Builder.Interfaces
{
    public interface IEntityFactory
    {
        public T Create<T>(IDbDataReader reader);
    }
}
