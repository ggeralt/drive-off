using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACarApi.Services;

namespace RentACarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpPut("Confirm"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> Confirm(int vehicleId)
        {
            var result = await adminService.VerifiedVehicleAsync(vehicleId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("GetAllNonConfirmedVehicles"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllNonConfirmedVehicles()
        {
            var result = await adminService.GetAllNonConfirmedVehiclesAsync();

            if (result == null)
            {
                return NotFound("There is no non confirmed vehicles");
            }

            return Ok(result);
        }
        
        [HttpGet("GetNonConfirmedVehicle"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetNonConfirmedVehicle(int vehicleId)
        {
            var result = await adminService.GetNonConfirmedVehicleAsync(vehicleId);

            if (result == null)
            {
                return NotFound("Failed to find vehicles");
            }

            return Ok(result);
        }

        [HttpDelete("DeleteAccount"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var result = await adminService.DeleteAccount(id);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteVehicle"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVehicle(int vehicleId)
        {
            var result = await adminService.DeleteVehicleAsync(vehicleId);
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
