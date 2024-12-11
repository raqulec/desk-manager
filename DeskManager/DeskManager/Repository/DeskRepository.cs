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
                .Include(d => d.Reservations)
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
                .Include(d => d.Reservations)
                .AsQueryable();

            foreach (var property in typeof(DeskFilter).GetProperties())
            {
                var value = property.GetValue(filter);
                if (value == null) continue;

                switch (property.Name)
                {
                    case nameof(DeskFilter.DeskNumber):
                        query = query.Where(d => d.DeskNumber == (int)value);
                        break;
                    case nameof(DeskFilter.RoomName):
                        query = query.Where(d => d.RoomName.Contains((string)value));
                        break;
                    case nameof(DeskFilter.IsAvailable):
                        query = query.Where(d => d.IsAvailable == (bool)value);
                        break;
                    case nameof(DeskFilter.ReservedBy):
                        query = query.Where(d => d.Reservations.Any(r => r.ReservedBy.Contains((string)value)));
                        break;
                    case nameof(DeskFilter.ReservationDate):
                        query = query.Where(d => d.Reservations.Any(r => r.ReservationDate.Date == ((DateTime)value).Date));
                        break;
                }
            }

            return await query.ToListAsync();
        }
    }
}
