using DeskManager.Models;

namespace DeskManager.Services
{
    public interface IDeskService
    {
        Task<List<Desk>> GetDesksAsync();
        Task AddDesksAsync(List<Desk> desks);
        Task DeleteDesksAsync(List<Desk> desks);
    }
}
