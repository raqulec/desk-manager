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
            var existingDesks = await GetDesksAsync();

            foreach (var desk in desks)
            {
                if (existingDesks.Any(d => d.DeskNumber == desk.DeskNumber && d.RoomName == desk.RoomName))
                {
                    throw new ArgumentException($"Desk with Desk Number: {desk.DeskNumber} and Room Name: {desk.RoomName} already exists.");
                }
            }
            await _deskRepository.AddDesksAsync(desks);
        }

        public async Task DeleteDesksAsync(List<Desk> desks)
        {
            var existingDesks = await GetDesksAsync();

            foreach (var desk in desks)
            {
                if (!existingDesks.Any(d => d.DeskNumber == desk.DeskNumber && d.RoomName == desk.RoomName))
                {
                    throw new ArgumentException($"Desk with Desk Number: {desk.DeskNumber} and Room Name: {desk.RoomName} does not exist.");
                }
            }

            await _deskRepository.DeleteDesksAsync(desks);
        }

        public async Task UpdateDesksAsync(List<Desk> desks)
        {
            var existingDesks = await GetDesksAsync();

            var nonExistingDesks = desks.Where(d => !existingDesks.Any(ed => ed.Id == d.Id)).ToList();

            if (nonExistingDesks.Any())
            {
                throw new ArgumentException($"The following desks do not exist: {string.Join(", ", nonExistingDesks.Select(d => d.Id))}");
            }

            await _deskRepository.UpdateDesksAsync(desks);
        }

        public async Task<List<DeskDto>> GetDesksByFilter(DeskFilter filter)
        {
            var filterCount = typeof(DeskFilter)
                .GetProperties()
                .Count(p => p.GetValue(filter) != null);

            if (filterCount == 0) 
            {
                throw new ArgumentException("At least one filter parameter must be provided.");
            }

            var desks = await _deskRepository.GetDesksByFilter(filter);

            return _mapper.Map<List<DeskDto>>(desks);
        }
    }
}
