using MODEL.DTOs;
using MODEL.Entities;

namespace BAL.ISercices
{
    public interface IUserServices
    {
        Task CreateUser(CreateUserDTOs inputModel);
        Task<string> LoginUser(LoginUserDTOs inputModel);
        Task<IEnumerable<Users>> GetAllUsers();
        Task<Users> GetUserById(Guid id); 
        Task UpdateUser(UpdateUserDTOs inputModel);
        Task DeleteUser(Guid id);
    }
}
