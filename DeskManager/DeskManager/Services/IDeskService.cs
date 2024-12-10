using DeskManager.Models;
using DeskManager.Models.DTO;

namespace DeskManager.Services
{
    public interface IDeskService
    {
        Task<List<DeskDto>> GetDesksAsync();
        Task AddDesksAsync(List<Desk> desks);
        Task DeleteDesksAsync(List<Desk> desks);
    }
}
