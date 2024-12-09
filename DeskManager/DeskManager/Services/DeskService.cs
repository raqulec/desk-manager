using DeskManager.Models;
using DeskManager.Repository;

namespace DeskManager.Services
{
    public class DeskService : IDeskService
    {
        private readonly IDeskRepository _deskRepository;

        public DeskService(IDeskRepository deskRepository)
        {
            _deskRepository = deskRepository;
        }

        public async Task<List<Desk>> GetDesksAsync()
        {
            var availableDesks = _deskRepository.GetDesksAsync();

            return await availableDesks;
        }

        public async Task AddDesksAsync(List<Desk> desks)
        {
            await _deskRepository.AddDesksAsync(desks);
        }

        public async Task DeleteDesksAsync(List<Desk> desks)
        {
            await _deskRepository.DeleteDesksAsync(desks);
        }
    }
}
