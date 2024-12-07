using DeskManager.Models;
using DeskManager.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DeskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DesksController : ControllerBase
    {
        private readonly IDeskService _deskService;

        public DesksController(IDeskService deskService)
        {
            _deskService = deskService;
        }

        //zobezpieczyc - dostep dla zalogowanych
        [HttpGet("GetDesks")]
        public async Task<IActionResult> GetDesksAsync()
        {
            var availableDesks = await _deskService.GetDesksAsync();

            if (availableDesks.Count == 0)
            {
                return NotFound("No available desks.");
            }

            return Ok(availableDesks);
        }

        public async Task<IActionResult> AddDesksAsync([FromBody] List<Desk> desks)
        {
            try
            {
                await _deskService.AddDesksAsync(desks);
                return Ok("Desks added successfully.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error retrieving data from the database");
            }
        }

        //dodac filter inna niz get, POST i w body przekazywac filrt
        //[HttpGet("GetAvailableDesksByDate")]
        //public ActionResult<List<Desk>> GetAvailableDesksOnDate([FromQuery] DateTime date)
        //{
        //    var desks = _deskService.GetAvailableDesksOnDate(date);

        //    return Ok(desks);
        //}
    }
}
