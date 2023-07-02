using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACarApi.Services;
using RentACarShared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentACarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Reservation1Controller : ControllerBase
    {
        private IReservationService reservationService;

        public Reservation1Controller(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        // GET: api/<Reservation1Controller>
        [HttpGet("GetAllReservations"), Authorize]
        public async Task<IActionResult> GetAllReservations()
        {
            var result = await reservationService.GetAllReservationsAsync();
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        // GET api/<Reservation1Controller>/5
        [HttpGet("{reservationId}")]
        public async Task<IActionResult> GetReservation(int reservationId)
        {
            var result = await reservationService.GetReservationAsync(reservationId);
            if (result == null)
            {
                return NotFound();
            }

            if (result.Id == reservationId)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        // POST api/<Reservation1Controller>
        [HttpPost("AddReservation"), Authorize]
        public async Task<IActionResult> AddReservation(ReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await reservationService.CreateReservationAsync(model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else if (result.Errors != null)
                {
                    return BadRequest(result);
                }
                else if (result.Errors == null && !result.IsSuccess)
                {
                    return Conflict(result);
                }
            }

            return BadRequest("Some properties are not valid");
        }

        // POST api/<Reservation1Controller>/5
        [HttpPost("UpdateReservation")]
        public async Task<IActionResult> UpdateReservation(int reservationId, ReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await reservationService.UpdateReservationAsync(reservationId, model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }

            return BadRequest("Some properties are not valid");
        }

        // DELETE api/<Reservation1Controller>/5
        [HttpDelete("DeleteReservation"), Authorize]
        public async Task<IActionResult> DeleteReservation(int reservationId)
        {
            var result = await reservationService.DeleteReservationAsync(reservationId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
