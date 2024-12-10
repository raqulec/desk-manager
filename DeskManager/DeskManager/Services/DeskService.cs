using AutoMapper;
using DeskManager.Models;
using DeskManager.Models.DTO;
using DeskManager.Repository;

namespace DeskManager.Services
{
    public class DeskService : IDeskService
    {
        private readonly IDeskRepository _deskRepository;
        private readonly IMapper _mapper;

        public DeskService(IDeskRepository deskRepository, IMapper mapper)
        {
            _deskRepository = deskRepository;
            _mapper = mapper;
        }

        public async Task<List<DeskDto>> GetDesksAsync()
        {
            var desks = await _deskRepository.GetDesksAsync();

            return _mapper.Map<List<DeskDto>>(desks);
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
