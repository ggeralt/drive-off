using RentACarShared;

namespace RentACarApi.Services
{
    public interface IAdminService
    {
        Task<ManagerResponse> VerifiedVehicleAsync(int vehicleId);
        Task<List<VehicleViewModel>> GetAllNonConfirmedVehiclesAsync();
        Task<VehicleViewModel> GetNonConfirmedVehicleAsync(int vehicleId);
        Task<ManagerResponse> DeleteAccount(string id);
        Task<ManagerResponse> DeleteVehicleAsync(int vehicleId);

    }
}
