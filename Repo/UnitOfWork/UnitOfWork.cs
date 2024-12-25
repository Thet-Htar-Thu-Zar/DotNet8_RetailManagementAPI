using Microsoft.Extensions.Options;
using MODEL;
using MODEL.ApplicationConfig;
using REPOSITORY.Repositories.IRepositories;
using REPOSITORY.Repositories.Repositories;

namespace REPOSITORY.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private DataContext _dataContext;

        public UnitOfWork(DataContext dataContext, IOptions<Appsetting> appsettings)
        {
            _dataContext = dataContext;
            Appsetting = appsettings.Value;
            Product = new ProductRepository(dataContext);
            Sale = new SaleRepository(dataContext);
        }
        public IProductRepository Product { get; set; }
        public ISaleRepository Sale { get; set; }
        public Appsetting Appsetting { get; set; }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }
    }
}
