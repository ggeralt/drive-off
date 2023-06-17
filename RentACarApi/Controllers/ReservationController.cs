using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACarApi.Model;
using RentACarApi.Services;
using RentACarShared;

namespace RentACarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IReservationService reservationService;

        ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

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

        [HttpPost("AddReservation")]
        public async Task<IActionResult> AddReservation(string userId, int vehicleId, ReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await reservationService.CreateReservationAsync(userId, model);
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

        [HttpDelete("DeleteReservation")]
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

        [HttpGet("GetAllReservations")]
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
    }
}
