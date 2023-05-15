using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACarApi.Services;

namespace RentACarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService fileService;

        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpGet("{vehicleId}")]
        public async Task<IActionResult> GetVehicle(int vehicleId)
        {
            var result = await fileService.GetPictureAsync(vehicleId);

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
    }
}
