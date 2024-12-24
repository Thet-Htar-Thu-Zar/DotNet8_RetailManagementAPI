using MODEL;
using MODEL.Entities;
using REPOSITORY.Repositories.IRepositories;

namespace REPOSITORY.Repositories.Repositories
{
    internal class ProductRepository: GenericRepository<Products>, IProductRepository
    {
        public ProductRepository(DataContext context): base(context) { }
    }
}
