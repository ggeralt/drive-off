using RentACarApi.Model;
using RentACarShared;

namespace RentACarApi.Services
{
    public interface IVehicleService
    {
        Task<ManagerResponse> CreateVehicleAsync(string userId, VehicleViewModel model);
        Task<ManagerResponse> UpdateVehicleAsync(int vehicleId, VehicleViewModel model);
        Task<ManagerResponse> DeleteVehicleAsync(int vehicleId);
        Task<Vehicle> GetVehicleAsync(int vehicleId);
        Task<List<Vehicle>> GetAllVehiclesAsync();
    }
}
