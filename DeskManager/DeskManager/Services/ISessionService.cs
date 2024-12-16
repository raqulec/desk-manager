using DeskManager.Models;

namespace DeskManager.Services
{
    public interface ISessionService
    {
        Task<string> Authenticate(Login request);
    }
}
