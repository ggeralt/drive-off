using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentACarApi.Services;
using RentACarShared;

namespace RentACarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : Controller
    {
        private IVehicleService vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [HttpGet("{vehicleId}")]
        public async Task<IActionResult> GetVehicle(int vehicleId) 
        {
            var result = await vehicleService.GetVehicleAsync(vehicleId);

            if (result == null)
            {
                return NotFound("Vehicle not found or no certificate");
            }

            if (result.Id == vehicleId)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("AddVehicle"), Authorize]
        public async Task<IActionResult> AddVehicle(VehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await vehicleService.CreateVehicleAsync(model);
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

        [HttpPost("UpdateVehicle"), Authorize]
        public async Task<IActionResult> UpdateVehicle(VehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await vehicleService.UpdateVehicleAsync(model);
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

        [HttpDelete("DeleteVehicle"), Authorize]
        public async Task<IActionResult> DeleteVehicle(int vehicleId)
        {
            var result = await vehicleService.DeleteVehicleAsync(vehicleId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("GetAllVehicles")]
        public async Task<IActionResult> GetAllVehicles()
        {
            var result = await vehicleService.GetAllVehiclesAsync();

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpGet("GetSearchedVehicles")]
        public async Task<IActionResult> GetSearchedVehicles(string searchValue)
        {
            var result = await vehicleService.GetSearchedVehiclesAsync(searchValue);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpGet("GetReviews")]
        public async Task<IActionResult> GetReviews(int vehicleId)
        {
            var result = await vehicleService.GetReviewsAsync(vehicleId);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPost("CreateReview"), Authorize]
        public async Task<IActionResult> CreateReview(ReviewViewModel reviewViewModel)
        {
            var result = await vehicleService.AddReviewsAsync(reviewViewModel);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpDelete("DeleteReview"), Authorize]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var result = await vehicleService.DeleteReviewAsync(reviewId);

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
