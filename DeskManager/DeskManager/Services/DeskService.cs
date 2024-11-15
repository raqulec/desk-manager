using DeskManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeskManager.Services
{
    public class DeskService
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
