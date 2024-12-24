using REPOSITORY.Repositories.IRepositories;

namespace REPOSITORY.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IProductRepository Product {  get; }
        Task<int> SaveChangesAsync();
    }
}
