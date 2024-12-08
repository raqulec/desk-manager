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
        [HttpGet("get-desks")]
        public async Task<IActionResult> GetDesksAsync()
        {
            var availableDesks = await _deskService.GetDesksAsync();

            if (availableDesks.Count == 0)
            {
                return NotFound("No available desks.");
            }

            return Ok(availableDesks);
        }

        [HttpPost("add-desks")]
        public async Task<IActionResult> AddDesksAsync([FromBody] List<Desk> desks)
        {
            var existingDesks = await _deskService.GetDesksAsync();

            foreach (var desk in desks)
            {
                if (existingDesks.Any(d => d.DeskNumber == desk.DeskNumber && d.RoomName == desk.RoomName))
                {
                    return BadRequest($"Desk with Desk Number: {desk.DeskNumber} and Room Name: {desk.RoomName} already exists.");
                }
            }

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

        [HttpDelete("delete-desks")]
        public async Task<IActionResult> DeleteDesksAsync([FromBody] List<Desk> desks)
        {
            var existingDesks = await _deskService.GetDesksAsync();

            foreach (var desk in desks)
            {
                if (!existingDesks.Any(d => d.DeskNumber == desk.DeskNumber && d.RoomName == desk.RoomName))
                {
                    return BadRequest($"Desk with Desk Number: {desk.DeskNumber} and Room Name: {desk.RoomName} does not exist.");
                }
            }

            try
            {
                await _deskService.DeleteDesksAsync(desks);
                return Ok("Desks deleted successfully.");
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
