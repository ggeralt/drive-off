using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Crypto.Digests;
using RentACarApi.Model;
using RentACarShared;

namespace RentACarApi.Services
{
    public class VehicleService : IVehicleService
    {
        private ApplicationDbContext context;
        private UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public VehicleService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<ManagerResponse> CreateVehicleAsync(string userId, VehicleViewModel model)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());


            if (user == null)
            {
                return new ManagerResponse
                {
                    Message = "Failed to find userId",
                    IsSuccess = false
                };
            }
            else if (model == null)
            {
                return new ManagerResponse
                {
                    Message = "Vehicle model is null",
                    IsSuccess = false
                };
            }

            Vehicle vehicleModel = mapper.Map<Vehicle>(model);
            vehicleModel.ApplicationUser = user;

            context.Vehicles.Add(vehicleModel);

            var result = await context.SaveChangesAsync();
            if (result > 0)
            {
                return new ManagerResponse
                {
                    Message = "New vehicle Created",
                    IsSuccess = true
                };
            }
            else
            {
                return new ManagerResponse
                {
                    Message = "Failed to create new vehicle",
                    IsSuccess = false
                };
            }
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
            else
            {
                return new ManagerResponse
                {
                    Message = "Failed to delete vehicle",
                    IsSuccess = false
                };
            }
        }

        public async Task<Vehicle> GetVehicleAsync(int vehicleId)
        {
            var vehicle = await context.Vehicles.FindAsync(vehicleId);
            return vehicle;
        }

        public async Task<ManagerResponse> UpdateVehicleAsync(int vehicleId, VehicleViewModel model)
        {
            var vehicle = await context.Vehicles.FindAsync(vehicleId);
            if (vehicle == null)
            {
                return new ManagerResponse
                {
                    Message = "Failed to find vehicleId",
                    IsSuccess = false
                };
            }
            else if (model == null)
            {
                return new ManagerResponse
                {
                    Message = "Vehicle model is null",
                    IsSuccess = false
                };
            }

            vehicle.Title = model.Title;
            vehicle.HasCertificate = model.HasCertificate;
            vehicle.Model = model.Model;
            vehicle.Brand = model.Brand;
            vehicle.Type = model.Type;
            vehicle.Description = model.Description;
            vehicle.Location = model.Location;
            vehicle.Latitude = model.Latitude;
            vehicle.Longitude = model.Longitude;

            context.Vehicles.Update(vehicle);
            var result = await context.SaveChangesAsync();
            if (result > 0)
            {
                return new ManagerResponse
                {
                    Message = "Vehicle updated",
                    IsSuccess = true
                };
            }
            else
            {
                return new ManagerResponse
                {
                    Message = "Failed to update vehicle",
                    IsSuccess = false
                };
            }
        }
    }
}
