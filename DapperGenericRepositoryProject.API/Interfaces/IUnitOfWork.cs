namespace DapperGenericRepositoryProject.API.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }    
    }
}
