using DeskManager.Models;

namespace DeskManager.Services
{
    public interface IUserService
    {
        Task<User> CreateUser(User request);
        Task<string> AuthenticateUser(Login request);
    }
}
