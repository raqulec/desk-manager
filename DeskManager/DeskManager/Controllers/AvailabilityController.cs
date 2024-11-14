using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AvailabilityController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAvailableDesks()
        {
            //var availableDesks = await _context.Desks.Where(d => d.IsAvailable).ToListAsync();
            //return Ok(availableDesks);
            return Ok();
        }
    }
}
