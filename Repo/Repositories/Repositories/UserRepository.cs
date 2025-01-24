using MODEL;
using MODEL.Entities;
using REPOSITORY.Repositories.IRepositories;

namespace REPOSITORY.Repositories.Repositories
{
    internal class UserRepository : GenericRepository<Users>,IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext) { }
    }
}
