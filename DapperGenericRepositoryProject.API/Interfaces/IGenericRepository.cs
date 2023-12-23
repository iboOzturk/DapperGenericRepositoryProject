namespace DapperGenericRepositoryProject.API.Interfaces
{
    public interface IGenericRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(int id);
    }
}
