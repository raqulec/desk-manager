using DeskManager.Models;
using DeskManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userRepository)
        {
            _userService = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User request)
        {
            try
            {
                return Ok(await _userService.RegisterUser(request));
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
                var token = await _userService.LoginUser(request);

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                Response.Headers.Append("Authorization", token);

                return Ok(new { message = "Login successful" });
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
