using DeskManager.Models;
using DeskManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeskManager.Controllers
{
    [Route("reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateReservation([FromBody] Reservation request)
        //{
        //    try
        //    {
        //        await _reservationService.CreateReservation(request);
        //        return Ok("User register successfully.");
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "An unexpected error occurred.");
        //    }
        //}
    }
}
