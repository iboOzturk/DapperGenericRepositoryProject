namespace DapperGenericRepositoryProject.API.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }    
        ICategoryRepository Categories { get; }
    }
}
