using DapperGenericRepositoryProject.API.DTOs;
using DapperGenericRepositoryProject.API.Models;

namespace DapperGenericRepositoryProject.API.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetOldestProductsAsync();
        Task<List<ProductWithCategoryDto>> GetProductWithCategoryAsync();  
    }
}
