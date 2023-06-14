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

        [HttpPut("Confirm")]
        public async Task<IActionResult> Confirm(int vehicleId)
        {
            var result = await adminService.VerifiedVehicleAsync(vehicleId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("GetAllNonConfirmedVehicles")]
        public async Task<IActionResult> GetAllNonConfirmedVehicles()
        {
            var result = await adminService.GetAllNonConfirmedVehiclesAsync();

            if (result == null)
            {
                return NotFound("There is no non confirmed vehicles");
            }

            return Ok(result);
        }
        
        [HttpGet("GetNonConfirmedVehicle")]
        public async Task<IActionResult> GetNonConfirmedVehicle(int vehicleId)
        {
            var result = await adminService.GetNonConfirmedVehicleAsync(vehicleId);

            if (result == null)
            {
                return NotFound("Failed to find vehicles");
            }

            return Ok(result);
        }
    }
}
