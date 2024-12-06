using DeskManager.Models;

namespace DeskManager.Services
{
    public interface IUserService
    {
        Task<User> RegisterUser(User request);
        Task<string> LoginUser(Login request);
    }
}
