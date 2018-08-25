namespace Domain
{
    public interface IRepository<T>
    {
        void AddOrUpdate(T obj);
    }
}
