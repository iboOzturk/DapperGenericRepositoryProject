using Dapper;
using DapperGenericRepositoryProject.API.Interfaces;
using DapperGenericRepositoryProject.API.Models;

namespace DapperGenericRepositoryProject.API.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(Context context) : base(context)
        {
        }

        public async Task<List<Product>> GetOldestProductsAsync()
        {
            string query = "SELECT * FROM Products WHERE CreateDate <= DATEADD(MONTH, -1, GETDATE());";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<Product>(query);
                return values.ToList();
            }
        }
    }
}
