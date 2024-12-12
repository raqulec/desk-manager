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
            var desks = await _dbContext.Desks
                .ToListAsync();
            return desks;
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

        public async Task DeleteDesksAsync(int deskId)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var existingDesk = await _dbContext.Desks
                        .FirstOrDefaultAsync(d => d.Id == deskId);

                if (existingDesk == null)
                {
                    throw new Exception($"Desk with Id: {deskId} does not exist.");
                }

                _dbContext.Desks.Remove(existingDesk);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateDesksAsync(List<Desk> desks)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var desksToUpdate = new List<Desk>();
                foreach (var desk in desks)
                {
                    var existingDesk = await _dbContext.Desks
                        .FirstOrDefaultAsync(d => d.Id == desk.Id);

                    if (existingDesk == null)
                    {
                        throw new KeyNotFoundException($"Desk with Id: {desk.Id} does not exist.");
                    }

                    existingDesk.DeskNumber = desk.DeskNumber;
                    existingDesk.RoomName = desk.RoomName;
                }

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<Desk>> GetDesksByFilter(DeskFilter filter)
        {
            var query = _dbContext.Set<Desk>()
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.DeskNumber))
            {
                query = query.Where(d => d.DeskNumber == filter.DeskNumber);
            }

            if (!string.IsNullOrEmpty(filter.RoomName))
            {
                query = query.Where(d => d.RoomName == filter.RoomName);
            }

            return await query.ToListAsync();
        }
    }
}
