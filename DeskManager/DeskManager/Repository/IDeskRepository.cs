﻿using DeskManager.Models;

namespace DeskManager.Repository
{
    public interface IDeskRepository
    {
        Task<List<Desk>> GetDesksAsync();
        Task AddDesksAsync(List<Desk> desks);
        Task DeleteDesksAsync(int deskId);
        Task UpdateDesksAsync(List<Desk> desks);
        Task<List<Desk>> GetDesksByFilter(DeskFilter filter);
    }
}
