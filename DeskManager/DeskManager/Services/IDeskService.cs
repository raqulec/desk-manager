using DeskManager.Models;
using DeskManager.Models.DTO;

namespace DeskManager.Services
{
    public interface IDeskService
    {
        Task<List<DeskDto>> GetDesksAsync();
        Task AddDesksAsync(List<Desk> desks);
        Task DeleteDesksAsync(int deskId);
        Task UpdateDesksAsync(List<Desk> desks);
        Task<List<DeskDto>> GetDesksByFilter(DeskFilter filter);
    }
}
