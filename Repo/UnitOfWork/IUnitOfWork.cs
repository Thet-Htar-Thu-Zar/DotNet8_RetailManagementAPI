using REPOSITORY.Repositories.IRepositories;

namespace REPOSITORY.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IProductRepository Product {  get; }
        ISaleRepository Sale { get; }
        Task<int> SaveChangesAsync();
        IUserRepository User { get; }
    }
}
