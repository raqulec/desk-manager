using DeskManager.Models;
using Microsoft.AspNetCore.Mvc;
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
            await _dbContext.Desks.AddRangeAsync(desks);
            await _dbContext.SaveChangesAsync();
        }
    }
}
