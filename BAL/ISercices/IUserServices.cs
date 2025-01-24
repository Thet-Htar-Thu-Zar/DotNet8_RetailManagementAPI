using MODEL.DTOs;

namespace BAL.ISercices
{
    public interface IUserServices
    {
        Task CreateUser(CreateUserDTOs inputModel);
        Task<string> LoginUser(LoginUserDTOs inputModel);
    }
}
