using DeskManager.Models;
using DeskManager.Repository;
using DeskManager.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        //zmienic warste
        //w repozytorium polaczenie z baza, repository
        // w serwisie logika
        //token zmienic zeby zwracany w headerze
        
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User request)
        {
            try
            {
                return Ok(await _userRepository.Register(request));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            try
            {
                var token = await _userRepository.Login(request);

                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest(new { message = "Invalid email or password" });
                }

                HttpContext.Items.Add("token", token);

                return Ok(new { token });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Items.Remove("token");

            return Ok(new { message = "User logged out successfully" });
        }
    }
}
