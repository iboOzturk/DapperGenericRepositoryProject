using DapperGenericRepositoryProject.API.Interfaces;

namespace DapperGenericRepositoryProject.API.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IProductRepository productRepository)
        {
            Products = productRepository;
        }
        public IProductRepository Products {  get; }
    }
}
