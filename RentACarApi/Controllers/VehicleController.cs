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
                return NotFound();
            }

            if (result.Id == vehicleId)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("AddVehicle")]
        public async Task<IActionResult> AddVehicle(string userId, VehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await vehicleService.CreateVehicleAsync(userId, model);
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

        [HttpPost("UpdateVehicle")]
        public async Task<IActionResult> UpdateVehicle(int vehicleId, [FromBody] VehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await vehicleService.UpdateVehicleAsync(vehicleId, model);
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

        [HttpDelete("DeleteVehicle")]
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
    }
}
