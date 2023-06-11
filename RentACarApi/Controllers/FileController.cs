using Microsoft.AspNetCore.Mvc;
using RentACarApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // DELETE api/<FileController>/5
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int pictureId)
        {
            var result = await fileService.DeletePictureAsync(pictureId);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
