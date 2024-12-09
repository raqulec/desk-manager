using DeskManager.Models;

namespace DeskManager.Repository
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User?> GetUserByEmail(string email);
    }
}