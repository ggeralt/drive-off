using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACarApi.Model;
using RentACarApi.Services;
using RentACarShared;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace RentACarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService userService;
        private IConfiguration configuration;
        private SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        public AuthController(IUserService userService, IConfiguration configuration, SignInManager<ApplicationUser> signInManager, 
            ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.userService = userService;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.RegisterUser(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.LoginUser(model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            return BadRequest("Some property are not valid");
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            var result = await userService.ConfirmEmail(userId, token);

            if (result.IsSuccess)
            {
                return Redirect($"{configuration["ApplicationUrl"]}/View/ConfirmEmail.html");
            }

            return BadRequest(result);
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return NotFound();
            }

            var result = await userService.ForgetPassword(email);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.ResetPassword(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return BadRequest("Some property are not valid");
        }

        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var result = await userService.DeleteAccount(id);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetVehicleUser"), Authorize]
        public async Task<IActionResult> GetVehicleUser()
        {
            string userId = GetUserId();

            var vehicles = context.Vehicles.Include(v => v.Pictures).Where(v => v.ApplicationUser.Id == userId).ToList();
            List<VehicleViewModel> vehicleViewModels = new List<VehicleViewModel>();

            foreach (var vehicle in vehicles)
            {
                VehicleViewModel vehicleViewModel = mapper.Map<VehicleViewModel>(vehicle);
                List<PictureViewModel> pictures = mapper.Map<List<PictureViewModel>>(vehicle.Pictures);
                vehicleViewModel.PictureViewModels = pictures;
                vehicleViewModels.Add(vehicleViewModel);
            }

            return Ok(vehicleViewModels);
        }

        [HttpGet("GetUser"), Authorize]
        public async Task<IActionResult> GetUser()
        {
            string userId = GetUserId();

            var user = context.Users.SingleOrDefault(u => u.Id == userId);

            UserViewModel userViewModel = new UserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email
            };

            return Ok(userViewModel);
        }
        private string GetUserId()
        {
            return httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
