using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACarApi.Model;
using RentACarShared;

namespace RentACarApi.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public AdminService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<ManagerResponse> VerifiedVehicleAsync(int vehicleId)
        {
            var vehicle = await context.Vehicles.FindAsync(vehicleId);

            if (vehicle == null)
            {
                return new ManagerResponse
                {
                    Message = "Failed to find vehicle",
                    IsSuccess = false
                };
            }

            vehicle.HasCertificate = true;
            var result = await context.SaveChangesAsync();

            if (result > 0)
            {
                return new ManagerResponse
                {
                    Message = "Vehicle verified",
                    IsSuccess = true
                };
            }
            else
            {
                return new ManagerResponse
                {
                    Message = "Failed verified vwhicle",
                    IsSuccess = false
                };
            }
        }

        public async Task<List<VehicleViewModel>> GetAllNonConfirmedVehiclesAsync()
        {
            List<VehicleViewModel> vehicleViewModels = new List<VehicleViewModel>();

            var vehicles = await context.Vehicles.Include(v => v.Pictures).Where(v => v.HasCertificate == false).ToListAsync();

            if (vehicles == null)
            {
                return null;
            }

            foreach (var vehicle in vehicles)
            {
                VehicleViewModel vehicleViewModel = mapper.Map<VehicleViewModel>(vehicle);
                List<PictureViewModel> pictures = mapper.Map<List<PictureViewModel>>(vehicle.Pictures);
                vehicleViewModel.PictureViewModels = pictures;
                vehicleViewModels.Add(vehicleViewModel);
            }

            return vehicleViewModels;

        }

        public async Task<VehicleViewModel> GetNonConfirmedVehicleAsync(int vehicleId)
        {
            var vehicle = await context.Vehicles.Include(v => v.Pictures).SingleOrDefaultAsync(v => v.Id == vehicleId && v.HasCertificate == false);

            if (vehicle == null)
            {
                return null;
            }

            VehicleViewModel vehicleViewModel = mapper.Map<VehicleViewModel>(vehicle);
            List<PictureViewModel> pictures = mapper.Map<List<PictureViewModel>>(vehicle.Pictures);
            vehicleViewModel.PictureViewModels = pictures;

            return vehicleViewModel;
        }

        public async Task<ManagerResponse> DeleteAccount(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return new ManagerResponse
                {
                    Message = "User not found",
                    IsSuccess = false
                };
            }

            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return new ManagerResponse
                {
                    Message = "The account has been deleted",
                    IsSuccess = true
                };
            }

            return new ManagerResponse
            {
                Message = "User cannot be deleted",
                IsSuccess = false,
                Errors = result.Errors.Select(errors => errors.Description)
            };
        }

        public async Task<ManagerResponse> DeleteVehicleAsync(int vehicleId)
        {
                        
            var vehicle = await context.Vehicles.FindAsync(vehicleId);

            if (vehicle == null)
            {
                return new ManagerResponse
                {
                    Message = "Failed to fiend vehicleId",
                    IsSuccess = false
                };
            }
            context.Vehicles.Remove(vehicle);

            var result = await context.SaveChangesAsync();
            if (result > 0)
            {
                return new ManagerResponse
                {
                    Message = "Vehicle deleted",
                    IsSuccess = true
                };
            }
            
            return new ManagerResponse
            {
                Message = "Failed to delete vehicle",
                IsSuccess = false
            };
        }
    }
}
