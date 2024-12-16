using DeskManager.Models;
using DeskManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeskManager.Controllers
{
    [Route("sessions")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            try
            {
                var token = await _sessionService.Authenticate(request);

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

        //[HttpPost("logout")]
        //public IActionResult Logout()
        //{
        //    HttpContext.Items.Remove("token");

        //    return Ok(new { message = "User logged out successfully" });
        //}
    }
}
