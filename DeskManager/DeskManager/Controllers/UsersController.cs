using DeskManager.Models;
using DeskManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeskManager.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userRepository)
        {
            _userService = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User request)
        {
            try
            {
                return Ok(await _userService.CreateUser(request));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
