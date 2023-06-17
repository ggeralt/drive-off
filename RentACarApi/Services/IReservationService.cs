using RentACarApi.Model;
using RentACarShared;

namespace RentACarApi.Services
{
    public interface IReservationService
    {
        Task<Reservation> GetReservationAsync(int reservationId);
        Task<List<Reservation>> GetAllReservationsAsync();
        Task<ManagerResponse> CreateReservationAsync(string userId, ReservationViewModel model);
        Task<ManagerResponse> UpdateReservationAsync(int reservationId, ReservationViewModel model);
        Task<ManagerResponse> DeleteReservationAsync(int reservationId);
    }
}
