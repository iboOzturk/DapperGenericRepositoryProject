using DapperGenericRepositoryProject.API.Models;

namespace DapperGenericRepositoryProject.API.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetOldestProductsAsync();
    }
}
