using RentACarApi.Model;
using RentACarShared;

namespace RentACarApi.Services
{
    public interface IVehicleService
    {
        Task<ManagerResponse> CreateVehicleAsync(VehicleViewModel model);
        Task<ManagerResponse> UpdateVehicleAsync(VehicleViewModel model);
        Task<ManagerResponse> DeleteVehicleAsync(int vehicleId);
        Task<VehicleViewModel> GetVehicleAsync(int vehicleId);
        Task<List<VehicleViewModel>> GetAllVehiclesAsync();
        Task<List<VehicleViewModel>> GetSearchedVehiclesAsync(string searchValue);
        Task<List<ReviewViewModel>> GetReviewsAsync(int vehicleId);
        Task<ManagerResponse> AddReviewsAsync(ReviewViewModel reviewViewModel);
        Task<ManagerResponse> DeleteReviewAsync(int reviewId);
    }
}
