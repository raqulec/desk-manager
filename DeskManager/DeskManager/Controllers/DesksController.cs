using DeskManager.Models;
using DeskManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeskManager.Controllers
{
    [ApiController]
    [Route("desks")]
    public class DesksController : ControllerBase
    {
        private readonly IDeskService _deskService;

        public DesksController(IDeskService deskService)
        {
            _deskService = deskService;
        }

        //zobezpieczyc - dostep dla zalogowanych
        [HttpGet]
        public async Task<IActionResult> GetDesksAsync()
        {
            var availableDesks = await _deskService.GetDesksAsync();

            if (availableDesks.Count == 0)
            {
                return NotFound(new { message = "No available desks." });
            }

            return Ok(availableDesks);
        }

        [HttpPost]
        public async Task<IActionResult> AddDesksAsync([FromBody] List<Desk> desks)
        {
            try
            {
                await _deskService.AddDesksAsync(desks);
                return Ok(new { message = "Desks added successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred."
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDesksAsync([FromQuery] int deskId)
        {
            try
            {
                await _deskService.DeleteDesksAsync(deskId);
                return Ok(new { message = "Desks deleted successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred."
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDesksAsync([FromBody] List<Desk> desks)
        {
            try
            {
                await _deskService.UpdateDesksAsync(desks);
                return Ok(new { message = "Desks updated successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred."
                });
            }
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetDesksByFilter([FromBody] DeskFilter filter)
        {
            try
            {
                var desks = await _deskService.GetDesksByFilter(filter);

                if (!desks.Any())
                    return NotFound(new
                    {
                        message = "No desks found."
                    });

                return Ok(desks);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred:"
                });
            }
        }
    }
}
