using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentACarApi.Model;
using RentACarShared;

namespace RentACarApi.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AdminService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
    }
}
