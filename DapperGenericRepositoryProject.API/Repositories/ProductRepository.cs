using Dapper;
using DapperGenericRepositoryProject.API.DTOs;
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

        public async Task<List<ProductWithCategoryDto>> GetProductWithCategoryAsync()
        {
            string query = @"SELECT p.ProductId, p.name , p.description,
                            p.CreateDate, c.name , c.categoryId AS categoryId
                            FROM Products p INNER JOIN Categories c ON p.categoryId = c.categoryId";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ProductWithCategoryDto, Category, ProductWithCategoryDto>(
                    query,
                    (product, category) =>
                    {
                        product.Category = category;
                        return product;
                    },
                    splitOn: "name"
                );

                return values.ToList();
            }
        }
    }
}
