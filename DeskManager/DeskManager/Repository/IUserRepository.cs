using DeskManager.Models;

namespace DeskManager.Repository
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User request);
        Task<User> AuthenticateUser(Login request);
    }
}