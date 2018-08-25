namespace Application.Excution
{
    public interface IReposytory
    {
        void AddOrUpdate<T>(T obj);
    }
}
