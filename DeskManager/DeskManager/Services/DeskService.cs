using DeskManager.Models;
using DeskManager.Repository;
using Microsoft.EntityFrameworkCore;

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

        //public List<Desk> GetAvailableDesksOnDate(DateTime date)
        //{
        //    var availableDesks = _dbContext.Desks
        //        .Include(d => d.Reservations)
        //        .Where(d => d.IsAvailable || !d.Reservations.Any(r => r.ReservationDate == date))
        //        .ToList();

        //    var filteredDesks = new List<Desk>();

        //    foreach (var desk in availableDesks)
        //    {
        //        var reservationsOnDate = desk.Reservations.Where(r => r.ReservationDate == date).ToList();
        //        if (desk.IsAvailable || reservationsOnDate.Count == 0)
        //        {
        //            filteredDesks.Add(desk);
        //        }
        //    }

        //    return filteredDesks;
        //}
    }
}
