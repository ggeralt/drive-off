﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            List<Picture> pictures = mapper.Map<List<Picture>>(model.PictureViewModels);
            vehicleModel.Pictures = pictures;

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

        public async Task<VehicleViewModel> GetVehicleAsync(int vehicleId)
        {
            var vehicle = await context.Vehicles.Include(v => v.Pictures).Include(v => v.ApplicationUser).SingleOrDefaultAsync(v => v.Id == vehicleId);

            VehicleViewModel vehicleViewModel = mapper.Map<VehicleViewModel>(vehicle);
            List<PictureViewModel> pictures = mapper.Map<List<PictureViewModel>>(vehicle.Pictures);
            vehicleViewModel.PictureViewModels = pictures;

            return vehicleViewModel;
        }

        public async Task<ManagerResponse> UpdateVehicleAsync(int vehicleId, VehicleViewModel model)
        {
            var vehicle = await context.Vehicles.Include(v => v.ApplicationUser).SingleOrDefaultAsync(v => v.Id == vehicleId);
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

            //var vehicleModel = mapper.Map<Vehicle>(model);
            //vehicleModel.ApplicationUser = vehicle.ApplicationUser;
            //vehicleModel.Id = vehicle.Id;

            vehicle.Title = model.Title;
            vehicle.HasCertificate = model.HasCertificate;
            vehicle.Model = model.Model;
            vehicle.Brand = model.Brand;
            vehicle.Type = model.Type;
            vehicle.Description = model.Description;
            vehicle.Location = model.Location;
            vehicle.Longitude = model.Longitude;
            vehicle.Latitude = model.Latitude;
            List<Picture> pictures = mapper.Map<List<Picture>>(model.PictureViewModels);
            vehicle.Pictures = pictures;

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

        public async Task<List<VehicleViewModel>> GetAllVehiclesAsync()
        {
            List<VehicleViewModel> vehicleViewModels = new List<VehicleViewModel>();

            var vehicles = await context.Vehicles.Include(v => v.Pictures).ToListAsync();

            foreach (var vehicle in vehicles)
            {
                VehicleViewModel vehicleViewModel = mapper.Map<VehicleViewModel>(vehicle);
                List<PictureViewModel> pictures = mapper.Map<List<PictureViewModel>>(vehicle.Pictures);
                vehicleViewModel.PictureViewModels = pictures;
                vehicleViewModels.Add(vehicleViewModel);
            }

            return vehicleViewModels;
        }
    }
}
