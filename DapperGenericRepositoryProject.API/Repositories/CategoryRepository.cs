using DapperGenericRepositoryProject.API.Interfaces;
using DapperGenericRepositoryProject.API.Models;

namespace DapperGenericRepositoryProject.API.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(Context context) : base(context)
        {
        }
    }
}
