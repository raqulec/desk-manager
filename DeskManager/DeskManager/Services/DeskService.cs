using DeskManager.Models;
using Microsoft.EntityFrameworkCore;

namespace DeskManager.Services
{
    public class DeskService : IDeskService
    {
        private readonly ApplicationDbContext _dbContext;

        public DeskService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Desk> GetAvailableDesksOnDate(DateTime date)
        {
            var availableDesks = _dbContext.Desks
                .Include(d => d.Reservations)
                .Where(d => d.IsAvailable || !d.Reservations.Any(r => r.ReservationDate == date))
                .ToList();

            var filteredDesks = new List<Desk>();

            foreach (var desk in availableDesks)
            {
                var reservationsOnDate = desk.Reservations.Where(r => r.ReservationDate == date).ToList();
                if (desk.IsAvailable || reservationsOnDate.Count == 0)
                {
                    filteredDesks.Add(desk);
                }
            }

            return filteredDesks;
        }
    }
}
