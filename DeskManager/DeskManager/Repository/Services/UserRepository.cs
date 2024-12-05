using DeskManager.Models;
using DeskManager.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeskManager.Repository.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly JwtUtils _jwtUtils;

        public UserRepository(ApplicationDbContext dbContext, JwtUtils jwtUtils)
        {
            _dbContext = dbContext;
            _jwtUtils = jwtUtils;
        }

        public async Task<User> Register(User request)
        {
            if (_dbContext.Users.Any(u => u.Email == request.Email))
            {
                throw new InvalidOperationException("Email is already taken");
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            var result = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<string> Login(Login request)
        {
            var existingUser = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(request.Password, existingUser.Password))
            {
                throw new InvalidOperationException("Email or password is incorrect");
            }

            var token = _jwtUtils.GenerateJwtToken(existingUser);
            return token;
        }
    }
}
