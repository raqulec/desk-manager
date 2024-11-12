using DeskManager.Models;
using DeskManager.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtUtils _jwtUtils;

        public UsersController(ApplicationDbContext context, JwtUtils jwtUtils)
        {
            _context = context;
            _jwtUtils = jwtUtils;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User request)
        {
            if(_context.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest(new { message = "Email is already taken" });
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully"});
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);

            if(existingUser == null || !BCrypt.Net.BCrypt.Verify(request.Password, existingUser.Password))
            {
                return BadRequest(new { message = "Email or password is incorrect" });
            }

            var token = _jwtUtils.GenerateJwtToken(existingUser);

            return Ok(new { token });
        }
    }
}
