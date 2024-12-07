using DeskManager.Models;

namespace DeskManager.Repository
{
    public interface IDeskRepository
    {
        Task<List<Desk>> GetDesksAsync();
        Task AddDesksAsync(List<Desk> desks);
    }
}
