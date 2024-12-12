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

        [HttpPost("register")]
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            try
            {
                var token = await _userService.AuthenticateUser(request);

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                Response.Headers.Append("Authorization", token);

                return Ok(new { message = "Login successful" });
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

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Items.Remove("token");

            return Ok(new { message = "User logged out successfully" });
        }


        //dodac walidacje, zmienic obsluge exceptionow, przeniesc id, poczytac o dobrych praktykach REST, sprawdzac jeszcze raz co poszczegolne endpointy zwracaja, jak zwracamy biurka to bez rezerwacji, jak jest rezerwacja to oddzielny enopoint, wrocic do kursu zobaczyc obsluge exceptionow
    }
}
