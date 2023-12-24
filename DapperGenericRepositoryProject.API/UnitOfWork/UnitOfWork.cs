using DapperGenericRepositoryProject.API.Interfaces;

namespace DapperGenericRepositoryProject.API.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IProductRepository productRepository, ICategoryRepository category)
        {
            Products = productRepository;
            Categories = category;

        }
        public IProductRepository Products {  get; }

        public ICategoryRepository Categories { get; }
    }
}
