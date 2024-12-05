using DeskManager.Models;
using DeskManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DeskService _deskService;

        public AvailabilityController(ApplicationDbContext context, DeskService deskService)
        {
            _context = context;
            _deskService = deskService;
        }

        //zobezpieczyc - dostep dla zalogowanych
        [HttpGet("GetAllAvailableDesks")]
        public IActionResult GetAllAvailableDesks()
        {
            var availableDesks = _context.Desks.Where(d => d.IsAvailable).ToList();

            if (availableDesks.Count == 0)
            {
                return NotFound("No available desks.");
            }

            return Ok(availableDesks);
        }

        //dodac filter inna niz get, POST i w body przekazywac filrt
        [HttpGet("GetAvailableDesksByDate")]
        public ActionResult<List<Desk>> GetAvailableDesksOnDate([FromQuery] DateTime date)
        {
            var desks = _deskService.GetAvailableDesksOnDate(date);

            return Ok(desks);
        }
    }
}
