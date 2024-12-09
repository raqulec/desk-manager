using DeskManager.Models;
using Microsoft.EntityFrameworkCore;

namespace DeskManager.Repository
{
    public class DeskRepository : IDeskRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DeskRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Desk>> GetDesksAsync()
        {
            var availableDesks = await _dbContext.Desks.ToListAsync();
            return availableDesks;
        }

        public async Task AddDesksAsync(List<Desk> desks)
        {
            foreach (var desk in desks)
            {
                if (await _dbContext.Desks.AnyAsync(d => d.DeskNumber == desk.DeskNumber && d.RoomName == desk.RoomName))
                {
                    throw new Exception($"Desk with Desk Number: {desk.DeskNumber} and Room Name: {desk.RoomName} already exists.");
                }
            }
            await _dbContext.Desks.AddRangeAsync(desks);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteDesksAsync(List<Desk> desks)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var desksToRemove = new List<Desk>();
                foreach (var desk in desks)
                {
                    var existingDesk = await _dbContext.Desks
                        .FirstOrDefaultAsync(d => d.DeskNumber == desk.DeskNumber && d.RoomName == desk.RoomName);

                    if (existingDesk != null)
                    {
                        desksToRemove.Add(existingDesk);
                    }
                    else
                    {
                        throw new Exception($"Desk with Desk Number: {desk.DeskNumber} and Room Name: {desk.RoomName} does not exist.");
                    }
                }

                _dbContext.Desks.RemoveRange(desksToRemove);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
